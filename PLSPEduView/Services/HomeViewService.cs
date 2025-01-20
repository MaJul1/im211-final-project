using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

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
            NumberOfStudentRegistered = _student.GetCount(),

            GroupByMunicipality = _student.GetAllAsync()
                .GroupBy(s => s.Municipality)
                .OrderByDescending(s => s.Count())
                .ToDictionary(g => g.Key, g => g.Count()),

            GroupByProvince = _student.GetAllAsync()
                .GroupBy(s => s.Province)
                .OrderByDescending(s => s.Count())
                .ToDictionary(s => s.Key, s => s.Count()),

            GroupBySex = _student.GetAllAsync()
                .GroupBy(s => s.Sex)
                .OrderByDescending(s => s.Count())
                .ToDictionary(s => s.Key.ToString(), s => s.Count()),

            GroupByType = _student.GetAllAsync()
                .GroupBy(s => s.Type)
                .OrderByDescending(s => s.Count())
                .ToDictionary(s => s.Key.ToString(), s => s.Count())
        };

        return view;
    }
}
