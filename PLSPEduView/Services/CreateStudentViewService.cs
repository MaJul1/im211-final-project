using System;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Mvc.Rendering;
using PLSPEduView.Enums;
using PLSPEduView.Models;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class CreateStudentViewService
{
    private readonly ConfigurationService _configurationService;
    private readonly SkillRepository _skillRepository;
    private readonly CourseRepository _courseRepository;
    private readonly ProgramRepository _programRepository;
    private readonly DepartmentRepository _departmentRepository;

    public CreateStudentViewService
    (
        ConfigurationService configurationService,
        SkillRepository skillRepository, 
        CourseRepository courseRepository, 
        ProgramRepository programRepository,
        DepartmentRepository departmentRepository
    )
    {
        _configurationService = configurationService;
        _skillRepository = skillRepository;
        _courseRepository = courseRepository;
        _programRepository = programRepository;
        _departmentRepository = departmentRepository;
    }
    public CreateStudentViewModel GetCreateStudentViewModel()
        => GetCreateStudentViewModel(null);
    public CreateStudentViewModel GetCreateStudentViewModel(CreateStudentViewModel? currentModel)
    {
        CreateStudentViewModel model;
        
        if (currentModel == null)
        {
            model = new();
        }
        else
        {
            model = currentModel;
        }

        model.BirthDay = GetCurrentDateAndTime();

        model.SectionOptions = GetSectionOptions();

        model.CoursesOptions = GetCoursesOptions();

        model.YearLevelOptions = GetYearLevelOptions();

        model.SkillOptions = GetSkillOptions();

        model.ProgramOptions = GetProgramOptions();

        model.DepartmentOptions = GetDepartmentOptions();
        
        model.SexOptions = GetSexOptions();

        model.StudentTypeOption = GetTypeOptions();

        return model;     
    }

    private List<SelectListOption> GetSexOptions()
    {
        var options = new List<SelectListOption>()
        {
            new() {Value = SexType.MALE.ToString(), Text = "Male"},
            new() {Value = SexType.FEMALE.ToString(), Text = "Female"}
        };
        
        return options;
    }
    private List<SelectListOption> GetTypeOptions()
    {
        var options = new List<SelectListOption>()
        {
            new() {Value = StudentType.IRREGULAR.ToString(), Text = "Irregular"},
            new() {Value = StudentType.REGULAR.ToString(), Text = "Regular"}
        };

        return options;
    }

    public Student GetStudent(CreateStudentViewModel model)
    {
        Student student = new()
        {
            SchoolId = model.SchoolId,
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            BirthDay = model.BirthDay,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Barangay = model.Barangay,
            Municipality = model.Municipality,
            Province = model.Province,
            YearLevel = model.YearLevel,
            Section = model.Section,
            Sex = model.Sex,
            Type = model.Type,
            DateAdded = DateTime.Now
        };

        student.Program = _programRepository.GetById(int.Parse(model.Program))!;

        student.Department = _departmentRepository.GetById(int.Parse(model.Department))!;

        foreach(var s in model.SkillIds)
        {
            var skill = _skillRepository.GetById(int.Parse(s));
            
            if (skill == null) continue;

            student.Skills.Add(skill);
        }

        foreach(var c in model.CourseIds)
        {
            var course = _courseRepository.GetById(int.Parse(c));
            
            if (course == null) continue;

            student.Courses.Add(course);
        }

        return student;
    }

    private List<SelectListOption> GetDepartmentOptions()
    {
        var departments = _departmentRepository.GetAll();

        var options = new List<SelectListOption>();

        foreach(var d in departments)
        {
            options.Add(SelectListService.CreateSelectListOption(d.Id.ToString(), string.Join(" - ", d.Code, d.Description)));
        }

        return options;
    }

    private List<SelectListOption> GetProgramOptions()
    {
        var programs = _programRepository.GetAll();

        var options = new List<SelectListOption>();

        foreach (var p in programs)
        {
            options.Add(SelectListService.CreateSelectListOption(p.Id.ToString(), p.Description));
        }

        return options;
    }

    private List<SelectListOption> GetSkillOptions()
    {
        var skills = _skillRepository.GetAll();

        var options = new List<SelectListOption>();

        foreach(var s in skills)
        {
            options.Add(SelectListService.CreateSelectListOption(s.Id.ToString(), s.Description));
        }

        return options;
    }

    private List<SelectListOption> GetYearLevelOptions()
    {
        var list = new List<SelectListOption>()
        {
            new() {Value = "1", Text = "1"},
            new() {Value = "2", Text = "2"},
            new() {Value = "3", Text = "3"},
            new() {Value = "4", Text = "4"}
        };

        return list;
    }

    private List<SelectListOption> GetCoursesOptions()
    {
        var courses = _courseRepository.GetAll();

        var options = new List<SelectListOption>();

        foreach(var c in courses)
        {
            options.Add(SelectListService.CreateSelectListOption(c.Id.ToString(), string.Join(" - ", c.CourseCode, c.CourseDescription)));
        }

        return options;
    }

    private static DateOnly GetCurrentDateAndTime()
    {
        return DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
    }

    private List<SelectListOption> GetSectionOptions()
    {
        var sections = _configurationService.GetListOfCharSections();

        var options = new List<SelectListOption>();

        foreach(var s in sections)
        {
            options.Add(SelectListService.CreateSelectListOption(s, s));
        }

        return options;
    }

    
}
