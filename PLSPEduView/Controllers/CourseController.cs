using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;
using PLSPEduView.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;

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

        public async Task<ActionResult> Index()
        {
            var model = await _viewService.GetCourseViewModelAsync();

            return View(model);
        }

        public async Task<ActionResult> ViewCourse(int courseId)
        {

            var model = await _repository.GetByIdAsync(courseId);

            if (model == null)
            {
                return BadRequest($"Course with an id {courseId} cannot be found.");
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

            var course = _writeService.GetCourse(model);

            await _repository.CreateCourseAsync(course);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCourse(int itemid)
        {
            CourseWriteModel? model;

            if (TempData["InvalidCourseModel"] is string json)
            {
                model = JsonConvert.DeserializeObject<CourseWriteModel>(json);
            }
            else
            {
                var existingCourse = await _repository.GetByIdAsync(itemid);

                if (existingCourse == null)
                {
                    return NotFound("Course not found.");
                }
                
                model = _writeService.GetCourseWriteModel(existingCourse);
            }

            ViewData["CourseId"] = itemid;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(CourseWriteModel model, int itemid)
        {
            var course = _writeService.GetCourse(model);

            if (await _repository.IsExistsAsync(itemid) == false)
            {
                return NotFound("Course not found.");
            }

            await _repository.UpdateAsync(course, itemid);

            return RedirectToAction("ViewCourse", new {courseId = itemid});
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
