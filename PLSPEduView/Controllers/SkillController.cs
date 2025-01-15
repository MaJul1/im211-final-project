using Microsoft.AspNetCore.Mvc;
using PLSPEduView.Repository;
using PLSPEduView.Services;

namespace PLSPEduView.Controllers
{
    public class SkillController : Controller
    {
        private readonly SkillViewService _service;
        private readonly SkillRepository _repository;
        public SkillController(SkillViewService service, SkillRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        public ActionResult Index(string sortParam)
        {
            ConfigureSort(sortParam);

            var model = _service.GetSkillViewModel();

            model.Skills = model.Skills.ApplySort(sortParam);
            
            return View(model);
        }

        public ActionResult ViewSkill(int itemid, string? sortParam)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortParam) ? "Id" : "";
            ViewData["NameSortParam"] = sortParam == "Name" ? "Name_desc" : "Name";
            ViewData["YearAndSectionSortParam"] = sortParam == "YearAndSection" ? "YearAndSection_desc" : "YearAndSection";
            ViewData["ProgramSortParam"] = sortParam == "Program" ? "Program_desc" : "Program";
            ViewData["DepartmentSortParam"] = sortParam == "Department" ? "Department_desc" : "Department";

            var skill = _repository.GetById(itemid);

            if (skill == null)
            {
                return BadRequest("Skill with an id of {itemid} does not exists.");
            }

            if (string.IsNullOrEmpty(sortParam) == false)
            {
                skill.Students = skill.Students.ApplySort(sortParam).ToList();
            }

            return View(skill);
        }

        private void ConfigureSort(string sortParam)
        {
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortParam) ? "Name" : "";
            ViewData["NumberOfStudentSortParam"] = sortParam == "NumberOfStudents" ? "NumberOfStudents_desc" : "NumberOfStudents";
        }
    }
}
