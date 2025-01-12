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
        StudentViewModel model = new()
        {
            Students = _studentRepository.GetAll(),
            SectionOptions = _configurationService.GetSelectListSection(),
            DepartmentOptions = _configurationService.GetSelectListDepartment(),
            ProgramOptions = _configurationService.GetSelectListProgram()
        };

        return model;
    }
}
