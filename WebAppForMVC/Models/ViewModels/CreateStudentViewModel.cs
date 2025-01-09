using System;
using System.ComponentModel.DataAnnotations;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Models.ViewModels;

public class CreateStudentViewModel
{
    [Required]
    public string SchoolId {get; set;} = null!;

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters.")]
    public string FirstName {get; set;} = null!;

    [StringLength(50, ErrorMessage = "Middle name cannot exceed 50 characters.")]
    public string MiddleName {get; set;} = null!;

    [Required(ErrorMessage = "Last name is required..")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public string LastName {get; set;} = null!;

    [DataType(DataType.Date)]
    public DateOnly BirthDay {get; set;}

    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    public int Age {get; set;}

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email {get; set;} = null!;

    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string PhoneNumber {get; set;} = null!;

    [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
    public string Address {get; set;} = null!;

    [Range(1, 12, ErrorMessage = "Year level must be between 1 and 12.")]
    public int YearLevel {get; set;}

    [RegularExpression("[A-Z]", ErrorMessage = "Section must be a single uppercase letter.")]
    public char Section {get; set;}

    [StringLength(100, ErrorMessage = "Program cannot exceed 100 characters.")]
    public string Program {get; set;} = null!;

    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters.")]
    public string Department {get; set;} = null!;

    [RegularExpression("Male|Female|Other", ErrorMessage = "Sex must be 'Male', 'Female', or 'Other'.")]
    public string Sex {get; set;} = null!;

    [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
    public string Type {get; set;} = null!;
    public List<string> CourseIds {get; set;} = null!;
    public List<string> SkillIds{get; set;} = null!;


    public IEnumerable<Course> CoursesOptions {get; set;} = null!;
    public IEnumerable<Skill> SkillOptions {get; set;} = null!;
}
