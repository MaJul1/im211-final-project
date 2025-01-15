using System;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Models.ViewModels;

public class SkillViewModel
{
    public IEnumerable<Skill> Skills {get; set;} = [];
}
