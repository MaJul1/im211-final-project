using Microsoft.AspNetCore.Mvc;
using WebAppForMVC.Services;

namespace WebAppForMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentViewService _studentViewService;

        public StudentController(StudentViewService studentViewService)
        {
            _studentViewService = studentViewService;
        }

        // GET: StudentController
        public ActionResult Index()
        {
            var model = _studentViewService.GetStudentViewModel();
            return View(model);
        }

    }
}
