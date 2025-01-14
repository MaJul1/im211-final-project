using Microsoft.AspNetCore.Mvc;
using WebAppForMVC.Repository;
using WebAppForMVC.Services;

namespace WebAppForMVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseViewService _service;
        private readonly CourseRepository _repository;

        public CourseController (CourseViewService service, CourseRepository repository)
        {
            _service = service;
            _repository = repository;
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

        //ToDo: Sorting
        public ActionResult ViewCourse(string courseId, string? sortParam)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortParam) ? "Id" : "";
            ViewData["NameSortParam"] = sortParam == "Name" ? "Name_desc" : "Name";
            ViewData["YearAndSectionSortParam"] = sortParam == "YearAndSection" ? "YearAndSection_desc" : "YearAndSection";
            ViewData["ProgramSortParam"] = sortParam == "Program" ? "Program_desc" : "Program";
            ViewData["DepartmentSortParam"] = sortParam == "Department" ? "Department_desc" : "Department";
            
            var model = _repository.GetById(Guid.Parse(courseId));

            if (model == null)
            {
                return BadRequest($"Course with an id {courseId} cannot be found.");
            }

            if (string.IsNullOrEmpty(sortParam) == false)
            {
                model.Students = [..model.Students.ApplySort(sortParam)];
            }

            return View(model);
        }

    }
}
