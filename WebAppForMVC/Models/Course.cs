using System;

namespace WebAppForMVC.Models;

public class Course
{
    public Guid Id {get; set;}
    public string CourseCode {get; set;} = null!;
    public string CourseDescription {get; set;} = null!;
    public int Units {get; set;}
    public DateTime DateAdded{get; set;}

    public ICollection<Student> Students {get; set;} = null!;
}

