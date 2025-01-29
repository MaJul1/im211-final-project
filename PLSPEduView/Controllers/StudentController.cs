using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;
using PLSPEduView.Services;

namespace PLSPEduView.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentViewService _viewService;
        private readonly StudentRepository _repository;
        private readonly StudentWriteModelService _viewModelService;
        public StudentController
        (
            StudentViewService service, 
            StudentRepository repository,
            StudentWriteModelService createService
        )
        {
            _viewService = service;
            _repository = repository;
            _viewModelService = createService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var studentViewModel = HttpContext.Session.GetString("StudentViewModel");

            if (studentViewModel is string json)
            {
                var model = JsonSerializer.Deserialize<StudentViewModel>(json);

                model = await _viewService.ReGenerateStudentViewModelAsync(model!);

                model.Students = model.Students.ApplyFilter(model);

                ViewData["IsFiltered"] = "true";

                return View(model);
            }

            return View(await _viewService.CreateAsync());
        }

        [HttpPost]
        public IActionResult Index(StudentViewModel model)
        {
            var json = JsonSerializer.Serialize(model);

            HttpContext.Session.SetString("StudentViewModel", json);           

            return RedirectToAction("Index");
        }

        public IActionResult ResetFilterSession()
        {
            HttpContext.Session.Remove("StudentViewModel");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewStudent(int itemid)
        {
            if ( await _repository.ExistAsync(itemid) == false)
            {
                return NotFound($"Student with ID {itemid} not found.");
            }

            var student = await _repository.GetStudentByIdAsync(itemid);

            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> CreateStudent()
        {
            var tempData = TempData["InvalidStudentCreateModel"];

            if (tempData is string json)
            {
                var model = JsonSerializer.Deserialize<StudentWriteModel>(json);

                model = await _viewModelService.GetStudentWriteModelAsync(model!);

                return View(model);
            }
            
            return View(await _viewModelService.GetStudentWriteModelAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentWriteModel model)
        {
            if (ModelState.IsValid)
            {
                var student = await _viewModelService.GetStudentAsync(model);

                await _repository.CreateStudentAsync(student);

                return RedirectToAction("Index");
            }

            TempData["InvalidStudentCreateModel"] = JsonSerializer.Serialize(model);

            return RedirectToAction("CreateStudent");
        }

        public IActionResult CreateSuccess()
        {
            return View();
        }

        public async Task<IActionResult> UpdateStudent(int itemid)
        {
            StudentWriteModel? model;
            if (ViewData["InvalidUpdateModel"] is string json)
            {
                model = JsonSerializer.Deserialize<StudentWriteModel>(json);

                model = await _viewModelService.GetStudentWriteModelAsync(model!);
            }
            else
            {
                var student = await _repository.GetStudentByIdAsync(itemid);

                if (student == null)
                    return NotFound($"Student with an id of {itemid} not found.");

                model = await _viewModelService.GetStudentWriteModelAsync(student);
            }

            ViewData["StudentId"] = itemid;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudent(StudentWriteModel model, int itemid)
        {
            if (ModelState.IsValid == false)
            {
                ViewData["InvalidUpdateModel"] = JsonSerializer.Serialize(model);

                return RedirectToAction("UpdateStudent", new {itemid});
            }

            var student = await _viewModelService.GetStudentAsync(model);

            await _repository.UpdateAsync(student, itemid);

            return RedirectToAction("ViewStudent", new {itemid});
        }

        public async Task<IActionResult> DeleteStudent(int itemid)
        {
            await _repository.RemoveByIdAsync(itemid);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult FilterStudent(StudentViewModel model)
        {
            TempData["FilterStudent"] = JsonSerializer.Serialize(model);

            return RedirectToAction("FilterStudent");
        }

        public async Task<IActionResult> FilterStudent()
        {
            var model = new StudentViewModel();

            if (TempData["FilterStudent"] is string json)
            {
                model = JsonSerializer.Deserialize<StudentViewModel>(json);

            }
            
            model = await _viewService.ReGenerateStudentViewModelAsync(model!);

            return View(model);
        }
    }
}
