using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;
using PLSPEduView.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PLSPEduView.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseViewService _viewService;
        private readonly CourseRepository _repository;
        private readonly CourseWriteModelService _writeService;

        public CourseController
        (
            CourseViewService service,
            CourseRepository repository,
            CourseWriteModelService writeModelService
        )
        {
            _viewService = service;
            _repository = repository;
            _writeService = writeModelService;
        }

        public async Task<ActionResult> Index(string sortParam)
        {
            ViewData["CodeSortParam"] = string.IsNullOrEmpty(sortParam) ? "" : "Code";
            ViewData["DescriptionSortParam"] = sortParam == "Description" ? "Description_desc" : "Description";
            ViewData["NumberOfStudentsSortParam"] = sortParam == "NumberOfStudents" ? "NumberOfStudents_desc" : "NumberOfStudents";

            var model = await _viewService.GetCourseViewModelAsync();

            model.Courses = model.Courses.ApplySort(sortParam);

            return View(model);
        }

        public async Task<ActionResult> ViewCourse(int courseId, string? sortParam)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortParam) ? "Id" : "";
            ViewData["NameSortParam"] = sortParam == "Name" ? "Name_desc" : "Name";
            ViewData["YearAndSectionSortParam"] = sortParam == "YearAndSection" ? "YearAndSection_desc" : "YearAndSection";
            ViewData["ProgramSortParam"] = sortParam == "Program" ? "Program_desc" : "Program";
            ViewData["DepartmentSortParam"] = sortParam == "Department" ? "Department_desc" : "Department";

            var model = await _repository.GetByIdAsync(courseId);

            if (model == null)
            {
                return BadRequest($"Course with an id {courseId} cannot be found.");
            }

            if (string.IsNullOrEmpty(sortParam) == false)
            {
                model.Students = [.. model.Students.ApplySort(sortParam)];
            }

            return View(model);
        }

        public IActionResult CreateCourse()
        {
            var model = _writeService.GetCourseWriteModel();

            if (TempData["InvalidCourseModel"] is string json)
            {
                model = JsonConvert.DeserializeObject<CourseWriteModel>(json);

                model = _writeService.GetCourseWriteModel(model!);
            }

            if (TempData["CodeExists"] is string error)
            {
                ModelState.AddModelError("Code", error);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseWriteModel model)
        {
            if (ModelState.IsValid == false)
            {
                TempData["InvalidCourseModel"] = JsonConvert.SerializeObject(model);

                return RedirectToAction("CreateCourse");
            }

            if (await _repository.IsCodeExistsAsync(model.Code))
            {
                TempData["InvalidCourseModel"] = JsonConvert.SerializeObject(model);

                TempData["CodeExists"] = $"{model.Code} is already used, use another code.";

                return RedirectToAction("CreateCourse");
            }

            var course = _writeService.GetCourse(model);

            await _repository.CreateCourseAsync(course);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int itemid)
        {
            if (await _repository.IsExistsAsync(itemid) == false)
            {
                return NotFound($"User with an id of {itemid} not found");
            }

            await _repository.Remove(itemid);

            return RedirectToAction("Index");
        }

    }
}
