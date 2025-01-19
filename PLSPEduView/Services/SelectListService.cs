using Microsoft.AspNetCore.Mvc.Rendering;
using PLSPEduView.Enums;
using PLSPEduView.Models;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class SelectListService
{
    private readonly SkillRepository _skillRepository;
    private readonly ConfigurationService _configurationService;
    private readonly ProgramRepository _programRepository;
    private readonly CourseRepository _courseRepository;
    private readonly DepartmentRepository _departmentRepository;
    public SelectListService 
    (
        SkillRepository skillRepository,
        ConfigurationService configurationService,
        ProgramRepository programRepository,
        CourseRepository courseRepository,
        DepartmentRepository departmentRepository
    )
    {
        _skillRepository = skillRepository;
        _configurationService = configurationService;
        _programRepository = programRepository;
        _courseRepository = courseRepository;
        _departmentRepository = departmentRepository;
    }
    private SelectList CreateSelectList(IEnumerable<SelectListOption> options)
    {
        List<SelectListItem> items = [];

        foreach(var s in options)
        {
            items.Add(new SelectListItem() { Value = s.Value, Text = s.Text});
        }

        return new (items, "Value", "Text");
    }
    
    private SelectListOption CreateSelectListOption(string value, string text)
    {
        return new SelectListOption() {Value = value, Text = text};
    }

    public SelectList GetSexSelectList()
    {
        var options = new List<SelectListOption>()
        {
            new() {Value = SexType.MALE.ToString(), Text = "Male"},
            new() {Value = SexType.FEMALE.ToString(), Text = "Female"}
        };
        
        return CreateSelectList(options);
    }
    public SelectList GetStudentTypeSelectList()
    {
        var options = new List<SelectListOption>()
        {
            new() {Value = StudentType.IRREGULAR.ToString(), Text = "Irregular"},
            new() {Value = StudentType.REGULAR.ToString(), Text = "Regular"}
        };

        return CreateSelectList(options);
    }

    public SelectList GetDepartmentSelectList()
    {
        var departments = _departmentRepository.GetAll();

        var options = new List<SelectListOption>();

        foreach(var d in departments)
        {
            options.Add(CreateSelectListOption(d.Id.ToString(), string.Join(" - ", d.Code, d.Description)));
        }

        return CreateSelectList(options);
    }

    public SelectList GetProgramSelectList()
    {
        var programs = _programRepository.GetAll();

        var options = new List<SelectListOption>();

        foreach (var p in programs)
        {
            options.Add(CreateSelectListOption(p.Id.ToString(), string.Join(" - ", p.Code, p.Description)));
        }

        return CreateSelectList(options);
    }

    public SelectList GetSkillSelectList()
    {
        var skills = _skillRepository.GetAll();

        var options = new List<SelectListOption>();

        foreach(var s in skills)
        {
            options.Add(CreateSelectListOption(s.Id.ToString(), s.Description));
        }

        return CreateSelectList(options);
    }

    public SelectList GetYearLevelSelectList()
    {
        var list = new List<SelectListOption>()
        {
            new() {Value = "1", Text = "1"},
            new() {Value = "2", Text = "2"},
            new() {Value = "3", Text = "3"},
            new() {Value = "4", Text = "4"}
        };

        return CreateSelectList(list);
    }

    public SelectList GetCourseSelectList()
    {
        var courses = _courseRepository.GetAll();

        var options = new List<SelectListOption>();

        foreach(var c in courses)
        {
            options.Add(CreateSelectListOption(c.Id.ToString(), string.Join(" - ", c.CourseCode, c.CourseDescription)));
        }

        return CreateSelectList(options);
    }

    public SelectList GetSectionSelectList()
    {
        var sections = _configurationService.GetListOfCharSections();

        var options = new List<SelectListOption>();

        foreach(var s in sections)
        {
            options.Add(CreateSelectListOption(s, s));
        }

        return CreateSelectList(options);
    }
    

    public SelectList GetSortSelectList()
    {
        List<SelectListOption> options = [
            new() {Value = "Id", Text = "Id"},
            new() {Value = "Name", Text = "Name"},
            new() {Value = "YearAndSection", Text = "YearAndSection"},
            new() {Value = "Program", Text = "Program"},
            new() {Value = "Department", Text = "Department"}
        ];

        return CreateSelectList(options);
    }
}
