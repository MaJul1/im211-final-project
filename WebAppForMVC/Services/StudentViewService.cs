using System;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

public class StudentViewService
{
    private StudentRepository _studentRepository;
    private ConfigurationService _configurationService;
    public StudentViewService(StudentRepository repository, ConfigurationService configurationService)
    {
        _studentRepository = repository;
        _configurationService = configurationService;
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

        model.ProgramOptions = _configurationService.GetSelectListOptionProgram();

        model.DepartmentOptions = _configurationService.GetSelectListOptionDepartment();

        model.SectionOptions = _configurationService.GetSelectListOptionSections();

        return model;
    }
}
