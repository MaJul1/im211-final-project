using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForMVC.Enums;
using WebAppForMVC.Interface;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Models.ViewModels;

public class StudentViewModel : IStudentFilter, IStudentOption, IStudentSort
{
    public IEnumerable<Student> Students {get; set;} = [];

    //Filters
    [Display(Name = "Minimum Birthday")]
    public DateOnly? BirthdayMinimumFilter {get; set;}

    [Display(Name = "Maximum Birthday")]
    public DateOnly? BirthdayMaximumFilter {get; set;}

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
    public SelectList SectionOptions {get; set;} = null!;
    public SelectList DepartmentOptions {get; set;} = null!;
    public SelectList ProgramOptions {get; set;} = null!;

    //Sort
    public string IdSortParameter { get; set; } = "Id";
    public string NameSortParameter { get; set; } = "Name";
    public string YearAndSectionSortParameter { get; set; } = "YearAndSection";
    public string ProgramSortParameter { get; set; } = "Program";
    public string DepartmentSortParameter { get; set; } = "Department";
    public string? SortParameter { get; set; }
}
