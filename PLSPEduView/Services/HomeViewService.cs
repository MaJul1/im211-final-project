using System.Threading.Tasks;
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

    public async Task<HomeViewModel> GetHomeViewModel()
    {
        var students = await _student.GetAllAsync();

        HomeViewModel view = new()
        {
            NumberOfCoursesRegistered = await _course.GetCountAsync(),
            NumberOfSkillsRegistered = await _skill.GetCountAsync(),
            NumberOfStudentRegistered = await _student.GetCountAsync(),

            GroupByMunicipality = students
                .GroupBy(s => s.Municipality)
                .OrderByDescending(s => s.Count())
                .ToDictionary(g => g.Key, g => g.Count()),

            GroupByProvince = students
                .GroupBy(s => s.Province)
                .OrderByDescending(s => s.Count())
                .ToDictionary(s => s.Key, s => s.Count()),

            GroupBySex = students
                .GroupBy(s => s.Sex)
                .OrderByDescending(s => s.Count())
                .ToDictionary(s => s.Key.ToString(), s => s.Count()),

            GroupByType = students
                .GroupBy(s => s.Type)
                .OrderByDescending(s => s.Count())
                .ToDictionary(s => s.Key.ToString(), s => s.Count())
        };

        return view;
    }
}
