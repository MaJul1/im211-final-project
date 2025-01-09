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
    public CreateStudentViewService(SkillRepository skillRepository, CourseRepository courseRepository)
    {
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

            model.SectionOptions = new(new List<SelectListItem>
            {
                new() { Value = "A", Text = "A" },
                new() { Value = "B", Text = "B" },
                new() { Value = "C", Text = "C" },
                new() { Value = "D", Text = "D" },
                new() { Value = "E", Text = "E" },
                new() { Value = "F", Text = "F" },
                new() { Value = "G", Text = "G" }
            }, "Value", "Text");

            model.YearLevelOptions = new(new List<SelectListItem>
            {
                new() { Value = "1", Text = "1" },
                new() { Value = "2", Text = "2" },
                new() { Value = "3", Text = "3" },
                new() { Value = "4", Text = "4" },
            }, "Value", "Text");

            model.ProgramOptions = new(new List<SelectListItem>
            {
                new() { Value = "BPED", Text = "BPED" }
            }, "Value", "Text");

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
            }, "Value", "Text");

            model.StudentTypeOptions = new(new List<SelectListItem>()
            {
                new() { Value = StudentType.REGULAR.ToString(), Text = "Regular"},
                new() { Value = StudentType.IRREGULAR.ToString(), Text = "Irregular"}
            }, "Value", "Text");

            model.DepartmentOptions = new(new List<SelectListItem>()
            {
                new() { Value = "CHK", Text = "College Of Human Kinetics"}
            }, "Value", "Text");

        return model;
    }
}
