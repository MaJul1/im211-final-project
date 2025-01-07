using System;

namespace WebAppForMVC.Models.DataModels;

public class Skill
{
    public Guid Id {get; set;}
    public string Description {get; set;} = null!;
    public DateTime DateAdded {get; set;}

    public ICollection<Student> Students {get; set;} = null!;
}
