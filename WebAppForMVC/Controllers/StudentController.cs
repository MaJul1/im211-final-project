using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppForMVC.Repository;
using WebAppForMVC.Services;

namespace WebAppForMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentViewService _studentViewService;
        private readonly StudentRepository _studentRepository;

        public StudentController(StudentViewService studentViewService, StudentRepository repository)
        {
            _studentViewService = studentViewService;
            _studentRepository = repository;
        }

        // GET: StudentController
        public IActionResult Index(string sortParam)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortParam) ? "Id" : "";
            ViewData["NameSortParam"] = sortParam == "Name" ? "Name_desc": "Name";
            ViewData["YearAndSectionSortParam"] = sortParam == "YearAndSection" ? "YearAndSection_desc" : "YearAndSection";
            ViewData["ProgramSortParam"] = sortParam == "Program" ? "Program_desc" : "Program";
            ViewData["DepartmentSortParam"] = sortParam == "Department" ? "Department_desc" : "Department";
            
            var model = _studentViewService.GetStudentViewModel();

            switch (sortParam)
            {
                case "Id":
                    model.StudentProfiles = model.StudentProfiles.OrderBy(s => s.Id);
                break;
                case "Name":
                    model.StudentProfiles = model.StudentProfiles.OrderBy(s => s.FullName);
                break;
                case "Name_desc":
                    model.StudentProfiles = model.StudentProfiles.OrderByDescending(s => s.FullName);
                break;
                case "YearAndSection":
                    model.StudentProfiles = model.StudentProfiles.OrderBy(s => s.YearAndSection);
                break;
                case "YearAndSection_desc":
                    model.StudentProfiles = model.StudentProfiles.OrderByDescending(s => s.YearAndSection);
                break;
                case "Program":
                    model.StudentProfiles = model.StudentProfiles.OrderBy(s => s.Program);
                break;
                case "Program_desc":
                    model.StudentProfiles = model.StudentProfiles.OrderByDescending(s => s.Program);
                break;
                case "Department":
                    model.StudentProfiles = model.StudentProfiles.OrderBy(s => s.Department);
                break;
                case "Department_desc":
                    model.StudentProfiles = model.StudentProfiles.OrderByDescending(s => s.Department);
                break;

            }

            return View(model);
        }

        public IActionResult CreateStudent()
        {
            return View();
        }

        public IActionResult ViewStudent(Guid itemid)
        {
            if (_studentRepository.Exist(itemid) == false)
            {
                return NotFound($"Student with ID {itemid} not found.");
            }

            var student =  _studentRepository.GetStudentById(itemid);
            
            return View(student);
        }

        public IActionResult UpdateStudent(Guid itemid)
        {
            return View();
        }

    }
}
