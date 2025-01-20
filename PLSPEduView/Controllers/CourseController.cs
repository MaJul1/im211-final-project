using Microsoft.AspNetCore.Mvc;
using PLSPEduView.Repository;
using PLSPEduView.Services;

namespace PLSPEduView.Controllers
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
        public ActionResult ViewCourse(int courseId, string? sortParam)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortParam) ? "Id" : "";
            ViewData["NameSortParam"] = sortParam == "Name" ? "Name_desc" : "Name";
            ViewData["YearAndSectionSortParam"] = sortParam == "YearAndSection" ? "YearAndSection_desc" : "YearAndSection";
            ViewData["ProgramSortParam"] = sortParam == "Program" ? "Program_desc" : "Program";
            ViewData["DepartmentSortParam"] = sortParam == "Department" ? "Department_desc" : "Department";
            
            var model = _repository.GetByIdAsync(courseId);

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
