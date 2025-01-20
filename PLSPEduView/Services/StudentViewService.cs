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

    public async Task<StudentViewModel> CreateAsync()
    {
        return await GenerateStudentViewModelAsync(null);
    }

    public async Task<StudentViewModel> ReGenerateStudentViewModelAsync(StudentViewModel model)
    {
        return await GenerateStudentViewModelAsync(model);
    }

    private async Task<StudentViewModel> GenerateStudentViewModelAsync(StudentViewModel? currentModel)
    {
        StudentViewModel model = currentModel ?? new();
        
        model.Students = await _studentRepository.GetAllAsync();

        model.ProgramOptions = await _selectListService.GetProgramSelectListAsync();

        model.DepartmentOptions = await _selectListService.GetDepartmentSelectListAsync();

        model.SectionOptions = await _selectListService.GetSectionSelectListAsync();

        model.SortOptions = await _selectListService.GetSortSelectListAsync();

        return model;
    }
}
