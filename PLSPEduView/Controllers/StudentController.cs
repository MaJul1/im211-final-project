using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var studentViewModel = TempData["StudentViewModel"];

            if (studentViewModel is string json)
            {
                var model = JsonSerializer.Deserialize<StudentViewModel>(json);

                model = _viewService.ReGenerateStudentViewModel(model!);

                model.Students = model.Students.ApplyFilter(model);
                
                model.Students = model.Students.ApplySort(model);

                return View(model);
            }

            return View(_viewService.Create());
        }

        [HttpPost]
        public IActionResult Index(StudentViewModel model)
        {
            TempData["StudentViewModel"] = JsonSerializer.Serialize(model);
            
            return RedirectToAction("Index");
        }

        public IActionResult ViewStudent(int itemid)
        {
            if (_repository.Exist(itemid) == false)
            {
                return NotFound($"Student with ID {itemid} not found.");
            }

            var student = _repository.GetStudentById(itemid);

            return View(student);
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            var tempData = TempData["InvalidStudentCreateModel"];

            if (tempData is string json)
            {
                var model = JsonSerializer.Deserialize<CreateStudentViewModel>(json);

                model = _createService.GetCreateStudentViewModel(model);

                return View(model);
            }
            
            return View(_createService.GetCreateStudentViewModel());
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _createService.GetStudent(model);

                _repository.CreateStudent(student);

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
