using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class StudentViewService
{
    private readonly StudentRepository _studentRepository;
    private readonly ConfigurationService _configurationService;
    private readonly ProgramRepository _programRepository;
    private readonly DepartmentRepository _departmentRepository;
    private readonly SelectListService _selectListService;
    public StudentViewService
    (
        SelectListService selectListService,
        StudentRepository repository, 
        ConfigurationService configurationService,
        ProgramRepository programRepository,
        DepartmentRepository departmentRepository
    )
    {
        _selectListService = selectListService;
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

        model.ProgramOptions = _selectListService.GetProgramSelectList();

        model.DepartmentOptions = _selectListService.GetDepartmentSelectList();

        model.SectionOptions = _selectListService.GetSectionSelectList();

        model.SortOptions = _selectListService.GetSortSelectList();

        return model;
    }
}
