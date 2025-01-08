using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

public class HomeViewService
{
    private readonly CourseRepository _course;
    private readonly SkillRepository _skill;
    private readonly StudentRepository _student;

    public HomeViewService
    (
        CourseRepository course,
        SkillRepository skill,
        StudentRepository student
    )
    {
        _course = course;
        _skill = skill;
        _student = student;
    }

    public HomeViewModel GetHomeViewModel()
    {
        HomeViewModel view = new()
        {
            NumberOfCoursesRegistered = _course.GetCount(),
            NumberOfSkillsRegistered = _skill.GetCount(),
            NumberOfStudentRegistered = _student.GetCount()
        };

        return view;
    }
}
