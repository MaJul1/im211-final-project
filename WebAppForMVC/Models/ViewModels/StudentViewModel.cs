using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForMVC.Enums;
using WebAppForMVC.Interface;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Services;

namespace WebAppForMVC.Models.ViewModels;

public class StudentViewModel : IStudentFilter, IStudentOption, ISortOption
{
    public IEnumerable<Student> Students {get; set;} = [];

    //Filters
    [Display(Name = "Minimum Age")]
    public int? AgeMinimumFilter {get; set;}

    [Display(Name = "Maximum Age")]
    public int? AgeMaximumFilter {get; set;}

    [Display(Name = "Municipality")]
    public string? MunicipalityFilter {get; set;}

    [Display(Name = "Province")]
    public string? ProvinceFilter {get; set;}

    [Display(Name = "Year Level")]
    public int? YearLevelFilter {get; set;}

    [Display(Name = "Section")]
    public char? SectionFilter {get; set;}

    [Display(Name = "Program")]
    public string? ProgramFilter {get; set;}

    [Display(Name = "Department")]
    public string? DepartmentFilter {get; set;}

    [Display(Name = "Sex")]
    public SexType? SexFilter {get; set;}

    [Display(Name = "Student Type")]
    public StudentType? TypeFilter {get; set;}
    public string? SearchParameter {get; set;}

    //Options
    public List<SelectListOption> SectionOptions {get; set;} = null!;
    public List<SelectListOption> DepartmentOptions {get; set;} = null!;
    public List<SelectListOption> ProgramOptions {get; set;} = null!;

    //Sort
    public string? SortParameter { get; set; }
    public bool IsDescending { get; set; }

    public SelectList GetSelectListSections()
    {
        return SelectListService.CreateSelectList(SectionOptions);
    }

    public SelectList GetSelectListDepartments()
    {
        return SelectListService.CreateSelectList(DepartmentOptions);
    }

    public SelectList GetSelectListPrograms()
    {
        return SelectListService.CreateSelectList(ProgramOptions);
    }

}
