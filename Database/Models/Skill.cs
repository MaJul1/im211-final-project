using System;

namespace Database.Models;

public class Skill
{
    public Guid Id {get; set;}
    public string Description {get; set;} = null!;

    public ICollection<Student> Students {get; set;} = null!;
}
