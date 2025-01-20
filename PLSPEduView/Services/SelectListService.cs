using System.Threading.Tasks;
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
    private async Task<SelectList> CreateSelectListAsync(IEnumerable<SelectListOption> options)
    {
        List<SelectListItem> items = [];

        foreach(var s in options)
        {
            items.Add(new SelectListItem() { Value = s.Value, Text = s.Text});
        }

        SelectList list = new(items, "Value", "Text");

        return await Task.FromResult(list);
    }
    
    private SelectListOption CreateSelectListOption(string value, string text)
    {
        return new SelectListOption() {Value = value, Text = text};
    }

    public async Task<SelectList> GetSexSelectListAsync()
    {
        var options = new List<SelectListOption>()
        {
            new() {Value = SexType.MALE.ToString(), Text = "Male"},
            new() {Value = SexType.FEMALE.ToString(), Text = "Female"}
        };
        
        return await CreateSelectListAsync(options);
    }
    public async Task<SelectList> GetStudentTypeSelectListAsync()
    {
        var options = new List<SelectListOption>()
        {
            new() {Value = StudentType.IRREGULAR.ToString(), Text = "Irregular"},
            new() {Value = StudentType.REGULAR.ToString(), Text = "Regular"}
        };

        return await CreateSelectListAsync(options);
    }

    public async Task<SelectList> GetDepartmentSelectListAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();

        var options = new List<SelectListOption>();

        foreach(var d in departments)
        {
            options.Add(CreateSelectListOption(d.Id.ToString(), string.Join(" - ", d.Code, d.Description)));
        }

        return await CreateSelectListAsync(options);
    }

    public async Task<SelectList> GetProgramSelectListAsync()
    {
        var programs = await _programRepository.GetAllAsync();

        var options = new List<SelectListOption>();

        foreach (var p in programs)
        {
            options.Add(CreateSelectListOption(p.Id.ToString(), string.Join(" - ", p.Code, p.Description)));
        }

        return await CreateSelectListAsync(options);
    }

    public async Task<SelectList> GetSkillSelectListAsync()
    {
        var skills = await _skillRepository.GetAllAsync();

        var options = new List<SelectListOption>();

        foreach(var s in skills)
        {
            options.Add(CreateSelectListOption(s.Id.ToString(), s.Description));
        }

        return await CreateSelectListAsync(options);
    }

    public async Task<SelectList> GetYearLevelSelectListAsync()
    {
        var list = new List<SelectListOption>()
        {
            new() {Value = "1", Text = "1"},
            new() {Value = "2", Text = "2"},
            new() {Value = "3", Text = "3"},
            new() {Value = "4", Text = "4"}
        };

        return await CreateSelectListAsync(list);
    }

    public async Task<SelectList> GetCourseSelectListAsync()
    {
        var courses = await _courseRepository.GetAllAsync();

        var options = new List<SelectListOption>();

        foreach(var c in courses)
        {
            options.Add(CreateSelectListOption(c.Id.ToString(), string.Join(" - ", c.CourseCode, c.CourseDescription)));
        }

        return await CreateSelectListAsync(options);
    }

    public async Task<SelectList> GetSectionSelectListAsync()
    {
        var sections = _configurationService.GetListOfCharSections();

        var options = new List<SelectListOption>();

        foreach(var s in sections)
        {
            options.Add(CreateSelectListOption(s, s));
        }

        return await CreateSelectListAsync(options);    
    }
    

    public async Task<SelectList> GetSortSelectListAsync()
    {
        List<SelectListOption> options = [
            new() {Value = "Id", Text = "Id"},
            new() {Value = "Name", Text = "Name"},
            new() {Value = "YearAndSection", Text = "YearAndSection"},
            new() {Value = "Program", Text = "Program"},
            new() {Value = "Department", Text = "Department"}
        ];

        return await CreateSelectListAsync(options);
    }
}
