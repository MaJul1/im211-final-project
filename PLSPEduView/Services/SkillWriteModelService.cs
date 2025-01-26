using System;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;

namespace PLSPEduView.Services;

public class SkillWriteModelService
{
    public SkillWriteModel GetSkillWriteModel()
        => GenerateSkillWriteModel(null);

    public SkillWriteModel GetSkillWriteModel(SkillWriteModel model)
        => GenerateSkillWriteModel(model);

    public SkillWriteModel GetSkillWriteModel(Skill skill)
    {
        var model = new SkillWriteModel()
        {
            Description = skill.Description
        };

        return model;
    }

    private SkillWriteModel GenerateSkillWriteModel(SkillWriteModel? existingModel)
    {
        SkillWriteModel model;

        if (existingModel == null)
        {
            model = new();
        }
        else
        {
            model = existingModel;
        }

        return model;
    }

    public Skill GetSkill(SkillWriteModel model)
    {
        Skill skill = new()
        {
            Description = model.Description,
            DateAdded = DateTime.Now
        };

        return skill;
    }
}
