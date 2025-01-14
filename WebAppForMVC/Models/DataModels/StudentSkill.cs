using System;

namespace WebAppForMVC.Models.DataModels;

public class StudentSkill
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public Guid SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
}
