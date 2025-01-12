using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;
using WebAppForMVC.Services;

namespace WebAppForMVC.Controllers
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
            return View(_service.Create());
        }

        [HttpPost]
        public IActionResult Index(StudentViewModel model)
        {
            model = _service.ReGenerateStudentViewModel(model);

            model.Students = model.Students.ApplyFilter(model);
            
            return View(model);
        }

        public IActionResult ViewStudent(Guid itemid)
        {
            if (_repository.Exist(itemid) == false)
            {
                return NotFound($"Student with ID {itemid} not found.");
            }

            var student = _repository.GetStudentById(itemid);

            return View(student);
        }
    }
}
