using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;
using WebAppForMVC.Services;

namespace WebAppForMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepository _studentRepository;

        public StudentController(StudentRepository repository)
        {
            _studentRepository = repository;
        }

        // GET: StudentController
        [HttpGet]
        public IActionResult Index(string sortParam, string searchParam)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortParam) ? "Id" : "";
            ViewData["NameSortParam"] = sortParam == "Name" ? "Name_desc" : "Name";
            ViewData["YearAndSectionSortParam"] = sortParam == "YearAndSection" ? "YearAndSection_desc" : "YearAndSection";
            ViewData["ProgramSortParam"] = sortParam == "Program" ? "Program_desc" : "Program";
            ViewData["DepartmentSortParam"] = sortParam == "Department" ? "Department_desc" : "Department";
            ViewData["CurrentSortString"] = searchParam;

            var model = _studentRepository.GetAll();

            if (searchParam != null)
            {
                model = model.Where(s => s.SchoolId.Contains(searchParam)
                                || string.Join(" ",s.FirstName, s.LastName).Contains(searchParam)
                                || string.Concat(s.YearLevel, s.Section).Contains(searchParam)
                                || s.Program.Contains(searchParam)
                                || s.Department.Contains(searchParam));
            }

            switch (sortParam)
            {
                case "Id":
                    model = model.OrderBy(s => s.Id);
                    break;
                case "Name":
                    model = model.OrderBy(s => s.FirstName)
                                .ThenBy(s => s.LastName);
                    break;
                case "Name_desc":
                    model = model.OrderByDescending(s => s.FirstName)
                                .ThenBy(s => s.LastName);
                    break;
                case "YearAndSection":
                    model = model.OrderBy(s => s.YearLevel)
                                .ThenBy(s => s.Section);
                    break;
                case "YearAndSection_desc":
                    model = model.OrderByDescending(s => s.YearLevel)
                                .ThenBy(s => s.Section);
                    break;
                case "Program":
                    model = model.OrderBy(s => s.Program);
                    break;
                case "Program_desc":
                    model = model.OrderByDescending(s => s.Program);
                    break;
                case "Department":
                    model = model.OrderBy(s => s.Department);
                    break;
                case "Department_desc":
                    model = model.OrderByDescending(s => s.Department);
                    break;
            }

            return View(model);
        }

        public IActionResult ViewStudent(Guid itemid)
        {
            if (_studentRepository.Exist(itemid) == false)
            {
                return NotFound($"Student with ID {itemid} not found.");
            }

            var student = _studentRepository.GetStudentById(itemid);

            return View(student);
        }
    }
}
