using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForMVC.Enums;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

public class CreateStudentViewService
{
    private readonly SkillRepository _skillRepository;
    private readonly CourseRepository _courseRepository;
    private readonly IConfiguration _configuration;
    public CreateStudentViewService(SkillRepository skillRepository, CourseRepository courseRepository, IConfiguration configuration)
    {
        _configuration = configuration;
        _skillRepository = skillRepository;
        _courseRepository = courseRepository;
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

            model.BirthDay = DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            model.SectionOptions = new
            (
                _configuration.GetSection("Sections")
                .Get<List<string>>()!
                .Select
                (
                    s => new SelectListItem{Value = s, Text = s}
                ), "Value", "Text"
            );

            model.YearLevelOptions = new(new List<SelectListItem>
            {
                new() { Value = "1", Text = "1" },
                new() { Value = "2", Text = "2" },
                new() { Value = "3", Text = "3" },
                new() { Value = "4", Text = "4" },
            }, "Value", "Text");

            model.ProgramOptions = new
            (
                _configuration.GetSection("Programs")
                .Get<List<string>>()!
                .Select(s => new SelectListItem
                {
                    Value = s,
                    Text = s
                }), "Value", "Text"
            );

            model.SkillOptions = new(_skillRepository.GetAll()
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Description
            }).OrderBy(s => s.Text), "Value", "Text");

            model.CoursesOptions = new(_courseRepository.GetAll()
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = string.Join(" - ", s.CourseCode, s.CourseDescription)
                }).OrderBy(s => s.Text), "Value", "Text");

            model.SexTypeOptions = new(new List<SelectListItem>()
            {
                new() { Value = SexType.MALE.ToString(), Text = "Male" },
                new() { Value = SexType.FEMALE.ToString(), Text = "Female" },
                new() { Value = SexType.OTHERS.ToString(), Text = "Others" },
            }, "Value", "Text");

            model.StudentTypeOptions = new(new List<SelectListItem>()
            {
                new() { Value = StudentType.REGULAR.ToString(), Text = "Regular"},
                new() { Value = StudentType.IRREGULAR.ToString(), Text = "Irregular"}
            }, "Value", "Text");

            model.DepartmentOptions = new
            ( 
                _configuration.GetSection("Departments")
                .Get<List<string>>()!
                .Select(s => new SelectListItem
                {
                    Value = s,
                    Text = s
                }), "Value", "Text"
            );

        return model;
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
            Program = model.Program,
            Department = model.Department,
            Sex = model.Sex,
            Type = model.Type,
            DateAdded = DateTime.Now
        };

        foreach(var s in model.SkillIds)
        {
            var skill = _skillRepository.GetById(Guid.Parse(s));
            
            if (skill == null) continue;

            student.Skills.Add(skill);
        }

        foreach(var c in model.CourseIds)
        {
            var course = _courseRepository.GetById(Guid.Parse(c));
            
            if (course == null) continue;

            student.Courses.Add(course);
        }

        return student;
    }
}
