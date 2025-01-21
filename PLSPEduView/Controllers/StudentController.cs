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
        private readonly CreateStudentViewService _createService;
        public StudentController
        (
            StudentViewService service, 
            StudentRepository repository,
            CreateStudentViewService createService
        )
        {
            _viewService = service;
            _repository = repository;
            _createService = createService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var studentViewModel = TempData["StudentViewModel"];

            if (studentViewModel is string json)
            {
                var model = JsonSerializer.Deserialize<StudentViewModel>(json);

                model = await _viewService.ReGenerateStudentViewModelAsync(model!);

                model.Students = model.Students.ApplyFilter(model);
                
                model.Students = model.Students.ApplySort(model);

                return View(model);
            }

            return View(await _viewService.CreateAsync());
        }

        [HttpPost]
        public IActionResult Index(StudentViewModel model)
        {
            TempData["StudentViewModel"] = JsonSerializer.Serialize(model);
            
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
                var model = JsonSerializer.Deserialize<CreateStudentViewModel>(json);

                model = await _createService.GetCreateStudentViewModelAsync(model);

                return View(model);
            }
            
            return View(await _createService.GetCreateStudentViewModelAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = await _createService.GetStudentAsync(model);

                await _repository.CreateStudentAsync(student);

                return RedirectToAction("CreateSuccess");
            }

            TempData["InvalidStudentCreateModel"] = JsonSerializer.Serialize(model);

            return RedirectToAction("CreateStudent");
        }

        public IActionResult CreateSuccess()
        {
            return View();
        }
    }
}
