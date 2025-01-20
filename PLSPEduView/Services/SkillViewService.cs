using System;
using System.Threading.Tasks;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class SkillViewService
{
    private readonly SkillRepository _repository;
    public SkillViewService(SkillRepository repository)
    {
        _repository = repository;
    }

    public async Task<SkillViewModel> GetSkillViewModel()
    {
        SkillViewModel model = new()
        {
            Skills = await _repository.GetAllAsync()
        };

        return model;
    }
}
