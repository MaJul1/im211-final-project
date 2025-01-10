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
    public string Program {get; set;} = null!;
    public string Department {get; set;} = null!;
    public SexType Sex {get; set;}
    public StudentType Type {get; set;}
    public DateTime DateAdded {get; set;}


    public ICollection<Skill> Skills {get; set;} = [];
    public ICollection<Course> Courses {get; set;} = [];
}
