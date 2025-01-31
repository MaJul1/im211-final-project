using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;
using PLSPEduView.Services;

namespace PLSPEduView.Controllers
{
    public class SkillController : Controller
    {
        private readonly SkillViewService _readService;
        private readonly SkillRepository _repository;
        private readonly SkillWriteModelService _writeService;
        public SkillController
        (
            SkillViewService readService, 
            SkillRepository repository,
            SkillWriteModelService writeService
        )
        {
            _readService = readService;
            _repository = repository;
            _writeService = writeService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _readService.GetSkillViewModel();
            
            return View(model);
        }

        public async Task<ActionResult> ViewSkill(int itemid)
        {

            var skill = await _repository.GetByIdAsync(itemid);

            if (skill == null)
            {
                return BadRequest($"Skill with an id of {itemid} does not exists.");
            }

            return View(skill);
        }

        public IActionResult CreateSkill()
        {
            return View(_writeService.GetSkillWriteModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill(SkillWriteModel model)
        {
            var skill = _writeService.GetSkill(model);

            await _repository.CreateSkillAsync(skill);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateSkill(int itemid)
        {
            var skill = await _repository.GetByIdAsync(itemid);

            if (skill == null)
            {
                return NotFound($"Skill with an id of {itemid} not found.");
            }

            var model = _writeService.GetSkillWriteModel(skill);

            ViewData["SkillId"] = itemid;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSkill(SkillWriteModel model, int itemid)
        {
            var newSkill = _writeService.GetSkill(model);

            if (await _repository.IsExistsAsync(itemid) == false)
            {
                return NotFound($"Skill with an id of {itemid} not found.");
            }

            await _repository.UpdateAsync(newSkill, itemid);

            return RedirectToAction("ViewSkill", new {itemid});
        }

        public async Task<IActionResult> DeleteSkill(int itemid)
        {
            if (await _repository.IsExistsAsync(itemid) == false)
            {
                return NotFound($"Skill with an id of {itemid} not found.");
            }

            await _repository.DeleteAsync(itemid);

            return RedirectToAction("Index");
        }
    }
}
