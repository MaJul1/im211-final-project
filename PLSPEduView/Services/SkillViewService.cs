using System;
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

    public SkillViewModel GetSkillViewModel()
    {
        SkillViewModel model = new()
        {
            Skills = _repository.GetAll()
        };

        return model;
    }
}
