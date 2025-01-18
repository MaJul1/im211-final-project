using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;
using PLSPEduView.Services;

namespace PLSPEduView.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentViewService _service;
        private readonly StudentRepository _repository;
        public StudentController(StudentViewService service, StudentRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var studentViewModel = TempData["StudentViewModel"];

            if (studentViewModel is string json)
            {
                var model = JsonSerializer.Deserialize<StudentViewModel>(json);

                model!.Students = model.Students.ApplySort(model);

                return View(model);
            }

            return View(_service.Create());
        }

        [HttpPost]
        public IActionResult Index(StudentViewModel model)
        {
            model = _service.ReGenerateStudentViewModel(model);

            model.Students = model.Students.ApplyFilter(model);

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

        public IActionResult CreateStudent()
        {
            return View();
        }
    }
}
