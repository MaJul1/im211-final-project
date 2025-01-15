using System;
using PLSPEduView.Models.DataModels;

namespace PLSPEduView.Models.ViewModels;

public class SkillViewModel
{
    public IEnumerable<Skill> Skills {get; set;} = [];
}
