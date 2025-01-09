using System;
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
    {
        CreateStudentViewModel model = new()
        {
            SkillOptions = _skillRepository.GetAll(),
            CoursesOptions = _courseRepository.GetAll()
        };

        return model;
    }
}
