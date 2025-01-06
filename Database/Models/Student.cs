using System;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using Database.Enums;

namespace Database.Models;

public class Student
{
    public Guid Id {get; set;}
    public string FirstName {get; set;} = null!;
    public string MiddleName {get; set;} = null!;
    public string LastName {get; set;} = null!;
    public DateOnly BirthDay {get; set;}
    public int Age {get; set;}
    public string Email {get; set;} = null!;
    public string PhoneNumber {get; set;} = null!;
    public string Address {get; set;} = null!;
    public int YearLevel {get; set;}
    public char Section {get; set;}
    public string Program {get; set;} = null!;
    public Sex Sex {get; set;}
    public StudentType Type {get; set;}

    public ICollection<Skill> Skills {get; set;} = null!;
    public ICollection<Course> Courses {get; set;} = null!;
}
