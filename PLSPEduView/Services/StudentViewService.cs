using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class StudentViewService
{
    private readonly StudentRepository _studentRepository;
    private readonly SelectListService _selectListService;
    public StudentViewService
    (
        SelectListService selectListService,
        StudentRepository repository
    )
    {
        _selectListService = selectListService;
        _studentRepository = repository;
    }

    public async Task<StudentViewModel> Create()
    {
        return await GenerateStudentViewModel(null);
    }

    public async Task<StudentViewModel> ReGenerateStudentViewModel(StudentViewModel model)
    {
        return await GenerateStudentViewModel(model);
    }

    private async Task<StudentViewModel> GenerateStudentViewModel(StudentViewModel? currentModel)
    {
        StudentViewModel model = currentModel ?? new();
        
        model.Students = await _studentRepository.GetAllAsync();

        model.ProgramOptions = await _selectListService.GetProgramSelectListAsync();

        model.DepartmentOptions = await _selectListService.GetDepartmentSelectListAsync();

        model.SectionOptions = _selectListService.GetSectionSelectListAsync();

        model.SortOptions = _selectListService.GetSortSelectListAsync();

        return model;
    }
}
