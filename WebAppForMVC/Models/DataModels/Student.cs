using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebAppForMVC.Enums;

namespace WebAppForMVC.Models.DataModels;

public class Student
{
    public Guid Id {get; set;}
    public string SchoolId {get; set;} = null!;
    public string FirstName {get; set;} = null!;
    public string MiddleName {get; set;} = null!;
    public string LastName {get; set;} = null!;
    public DateOnly BirthDay {get; set;}
    public string Email {get; set;} = null!;
    public string PhoneNumber {get; set;} = null!;
    public string Barangay {get; set;} = null!;
    public string Municipality {get; set;} = null!;
    public string Province {get; set;} = null!;
    public int YearLevel {get; set;}
    public char Section {get; set;}
    public SexType Sex {get; set;}
    public StudentType Type {get; set;}
    public DateTime DateAdded {get; set;}

    public SchoolProgram Program {get; set;} = null!;
    public Department Department {get; set;} = null!;

    public ICollection<Skill> Skills {get; set;} = [];
    public ICollection<Course> Courses {get; set;} = [];
    public ICollection<StudentSkill> StudentSkills {get; set;} = [];
    public ICollection<StudentCourse> StudentCourses {get; set;} = [];

    public int GetAge()
    {
        var today = DateTime.Today;
        var age = today.Year - BirthDay.Year;
        if (BirthDay.DayNumber > DateOnly.FromDateTime(today.AddYears(-age)).DayNumber) age--;
        return age;
    }

    public int GetTotalUnits()
    {
        var units = Courses.Select(s => s.Units);

        var total = 0;
        foreach (var u in units)
        {
            total += u;
        }

        return total;
    }
}
