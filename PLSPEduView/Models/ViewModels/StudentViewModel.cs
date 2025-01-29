using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PLSPEduView.Enums;
using PLSPEduView.Interface;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Services;

namespace PLSPEduView.Models.ViewModels;

public class StudentViewModel : IStudentFilter
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
    [JsonIgnore]
    public SelectList SectionOptions {get; set;} = null!;
    [JsonIgnore]
    public SelectList DepartmentOptions {get; set;} = null!;
    [JsonIgnore]
    public SelectList ProgramOptions {get; set;} = null!;
    [JsonIgnore]
    public SelectList SortOptions {get; set;} = null!;

}
