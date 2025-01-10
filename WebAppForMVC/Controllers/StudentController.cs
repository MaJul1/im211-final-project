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
        private readonly CreateStudentViewService _studentCreateService;

        public StudentController(StudentRepository repository, CreateStudentViewService studentCreateService)
        {
            _studentRepository = repository;
            _studentCreateService = studentCreateService;
        }

        // GET: StudentController
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
                                || s.FirstName.Contains(searchParam)
                                || s.LastName.Contains(searchParam)
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

        public IActionResult CreateStudent()
        {
            var viewModel = _studentCreateService.GetCreateStudentViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _studentRepository.CreateStudent(_studentCreateService.GetStudent(model));

                return RedirectToAction("Index");
            }

            var newModel = _studentCreateService.GetCreateStudentViewModel(model);

            return View(newModel);
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

        public IActionResult UpdateStudent(Guid itemid)
        {
            return View();
        }

        public IActionResult DeleteStudent(Guid itemid)
        {
            _studentRepository.RemoveById(itemid);

            return RedirectToAction("Index");
        }

    }
}
