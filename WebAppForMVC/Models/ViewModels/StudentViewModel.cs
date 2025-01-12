using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForMVC.Enums;
using WebAppForMVC.Interface;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Models.ViewModels;

public class StudentViewModel : IStudentFilter, IStudentOption, IStudentSort
{
    public IEnumerable<Student> Students {get; set;} = [];

    //Filters
    public DateOnly? BirthdayMinimumFilter {get; set;}
    public DateOnly? BirthdayMaximumFilter {get; set;}
    public int? AgeMinimumFilter {get; set;}
    public int? AgeMaximumFilter {get; set;}
    public string? MunicipalityFilter {get; set;}
    public string? ProvinceFilter {get; set;}
    public int? YearLevelFilter {get; set;}
    public char? SectionFilter {get; set;}
    public string? ProgramFilter {get; set;}
    public string? DepartmentFilter {get; set;}
    public SexType? SexFilter {get; set;}
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
