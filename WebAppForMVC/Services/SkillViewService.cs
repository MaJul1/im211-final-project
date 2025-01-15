using System;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

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
