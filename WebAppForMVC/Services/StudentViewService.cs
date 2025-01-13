using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

public class StudentViewService
{
    private readonly StudentRepository _studentRepository;
    private readonly ConfigurationService _configurationService;
    private readonly ProgramRepository _programRepository;
    private readonly DepartmentRepository _departmentRepository;
    public StudentViewService
    (
        StudentRepository repository, 
        ConfigurationService configurationService,
        ProgramRepository programRepository,
        DepartmentRepository departmentRepository
    )
    {
        _studentRepository = repository;
        _configurationService = configurationService;
        _programRepository = programRepository;
        _departmentRepository = departmentRepository;
    }

    public StudentViewModel Create()
    {
        return GenerateStudentViewModel(null);
    }

    public StudentViewModel ReGenerateStudentViewModel(StudentViewModel model)
    {
        return GenerateStudentViewModel(model);
    }

    private StudentViewModel GenerateStudentViewModel(StudentViewModel? currentModel)
    {
        StudentViewModel model = currentModel ?? new();
        
        model.Students = _studentRepository.GetAll();

        model.ProgramOptions = _programRepository.GetAsSelectListOptions();

        model.DepartmentOptions = _departmentRepository.GetAsSelectListOptions();

        model.SectionOptions = _configurationService.GetSelectListOptionSections();

        model.SortOptions = [
            new() {Value = "Id", Text = "Id"},
            new() {Value = "Name", Text = "Name"},
            new() {Value = "YearAndSection", Text = "YearAndSection"},
            new() {Value = "Program", Text = "Program"},
            new() {Value = "Department", Text = "Department"}
        ];

        return model;
    }
}
