using Microsoft.AspNetCore.Mvc;
using WebAppForMVC.Services;

namespace WebAppForMVC.Controllers
{
    public class SkillController : Controller
    {
        private readonly SkillViewService _service;
        public SkillController(SkillViewService service)
        {
            _service = service;
        }

        public ActionResult Index(string sortParam)
        {
            ConfigureSort(sortParam);

            var model = _service.GetSkillViewModel();

            model.Skills = model.Skills.ApplySort(sortParam);
            
            return View(model);
        }

        private void ConfigureSort(string sortParam)
        {
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortParam) ? "Name" : "";
            ViewData["NumberOfStudentSortParam"] = sortParam == "NumberOfStudents" ? "NumberOfStudents_desc" : "NumberOfStudents";
        }
    }
}
