using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using PLSPEduView.Enums;
using PLSPEduView.Services;

namespace PLSPEduView.Models.ViewModels;

public class CreateStudentViewModel
{
    [Required]
    [Display(Name = "School Id")]
    [Length(8, 8)]
    public string SchoolId { get; set; } = null!;

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [StringLength(50, ErrorMessage = "Middle name cannot exceed 50 characters.")]
    [Display(Name = "Middle Name")]
    public string MiddleName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required..")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [DataType(DataType.Date)]
    [Display(Name = "Birth Day")]
    public DateOnly BirthDay { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Invalid Phone Number")]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(100, ErrorMessage = "Barangay cannot exceed 100 characters.")]
    [Display(Name = "Barangay")]
    public string Barangay { get; set; } = null!;

    [StringLength(100, ErrorMessage = "Municipality cannot exceed 100 characters.")]
    [Display(Name = "Municipality")]
    public string Municipality { get; set; } = null!;

    [StringLength(100, ErrorMessage = "Province cannot exceed 100 characters.")]
    [Display(Name = "Province")]
    public string Province { get; set; } = null!;

    [Range(1, 4, ErrorMessage = "Year level must be between 1 and 12.")]
    [Display(Name = "Year Level")]
    public int YearLevel { get; set; }

    [RegularExpression("[A-Z]", ErrorMessage = "Section must be a single uppercase letter.")]
    [Display(Name = "Section")]
    public char Section { get; set; }

    [StringLength(100, ErrorMessage = "Program cannot exceed 100 characters.")]
    [Display(Name = "Program")]
    public string Program { get; set; } = null!;

    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters.")]
    [Display(Name = "Department")]
    public string Department { get; set; } = null!;

    [Display(Name = "Sex")]
    public SexType Sex { get; set; }

    [Display(Name = "Student Type")]
    public StudentType Type { get; set; }

    [Display(Name = "Courses Enrolled")]
    public List<string> CourseIds { get; set; } = null!;

    [Display(Name = "Skills")]
    public List<string> SkillIds { get; set; } = null!;

    [ValidateNever]
    public List<SelectListOption> CoursesOptions { get; set; } = null!;
    [ValidateNever]
    public List<SelectListOption> SkillOptions { get; set; } = null!;
    [ValidateNever]
    public List<SelectListOption> YearLevelOptions { get; set; } = null!;
    [ValidateNever]
    public List<SelectListOption> SectionOptions { get; set; } = null!;
    [ValidateNever]
    public List<SelectListOption> ProgramOptions { get; set; } = null!;
    [ValidateNever]
    public List<SelectListOption> DepartmentOptions {get; set;} = null!;
    [ValidateNever]
    public List<SelectListOption> SexOptions {get; set;} = null!;
    [ValidateNever]
    public List<SelectListOption> StudentTypeOption {get; set;} = null!;

    public SelectList GetSelectListCourses()
    {
        return SelectListService.CreateSelectList(CoursesOptions);
    }
    public SelectList GetSelectListPrograms()
    {
        return SelectListService.CreateSelectList(ProgramOptions);
    }
    public SelectList GetSelectListSkills()
    {
        return SelectListService.CreateSelectList(SkillOptions);
    }
    public SelectList GetSelectListYearLevel()
    {
        return SelectListService.CreateSelectList(YearLevelOptions);
    }
    public SelectList GetSelectListSections()
    {
        return SelectListService.CreateSelectList(SectionOptions);
    }
    public SelectList GetSelectListDepartment()
    {
        return SelectListService.CreateSelectList(DepartmentOptions);
    }
    public SelectList GetSelectListSex()
    {
        return SelectListService.CreateSelectList(SexOptions);
    }
    public SelectList GetSelectListStudentType()
    {
        return SelectListService.CreateSelectList(StudentTypeOption);
    }
}
