using Microsoft.AspNetCore.Mvc;
using WebAppForMVC.Services;

namespace WebAppForMVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseViewService _service;

        public CourseController (CourseViewService service)
        {
            _service = service;
        }

        // GET: CourseController
        public ActionResult Index(string sortParam)
        {
            ViewData["CodeSortParam"] = string.IsNullOrEmpty(sortParam) ? "" : "Code";
            ViewData["DescriptionSortParam"] = sortParam == "Description" ? "Description_desc" : "Description";
            ViewData["NumberOfStudentsSortParam"] = sortParam == "NumberOfStudents" ? "NumberOfStudents_desc" : "NumberOfStudents";

            var model = _service.GetCourseViewModel();

            model.Courses = model.Courses.ApplySort(sortParam);
            
            return View(model);
        }

        public ActionResult ViewCourse(string itemid)
        {
            return View();
        }

    }
}
