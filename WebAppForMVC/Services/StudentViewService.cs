using System;
using WebAppForMVC.Mapper;
using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

public class StudentViewService
{
    private readonly StudentRepository _studentRepository;
    public StudentViewService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public StudentViewModel GetStudentViewModel()
    {
        StudentViewModel studentViewModel = new()
        {
            NumberOfStudentsRegistered = _studentRepository.GetCount(),
            StudentProfiles = _studentRepository.GetAll().Select(s => s.ToStudentProfile())
        };

        return studentViewModel;
    }
}
