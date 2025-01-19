using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class CreateStudentViewService
{
    private readonly SelectListService _selectListservice;
    private readonly SkillRepository _skillRepository;
    private readonly CourseRepository _courseRepository;
    private readonly ProgramRepository _programRepository;
    private readonly DepartmentRepository _departmentRepository;

    public CreateStudentViewService
    (
        SelectListService selectListService,
        SkillRepository skillRepository, 
        CourseRepository courseRepository, 
        ProgramRepository programRepository,
        DepartmentRepository departmentRepository
    )
    {
        _selectListservice = selectListService;
        _skillRepository = skillRepository;
        _courseRepository = courseRepository;
        _programRepository = programRepository;
        _departmentRepository = departmentRepository;
    }
    public CreateStudentViewModel GetCreateStudentViewModel()
        => GetCreateStudentViewModel(null);
    public CreateStudentViewModel GetCreateStudentViewModel(CreateStudentViewModel? currentModel)
    {
        CreateStudentViewModel model;
        
        if (currentModel == null)
        {
            model = new();
        }
        else
        {
            model = currentModel;
        }

        model.BirthDay = GetCurrentDateAndTime();

        model.SectionOptions = _selectListservice.GetSectionSelectList();

        model.CoursesOptions = _selectListservice.GetCourseSelectList();

        model.YearLevelOptions = _selectListservice.GetYearLevelSelectList();

        model.SkillOptions = _selectListservice.GetSkillSelectList();

        model.ProgramOptions = _selectListservice.GetProgramSelectList();

        model.DepartmentOptions = _selectListservice.GetDepartmentSelectList();
        
        model.SexOptions = _selectListservice.GetSexSelectList();

        model.StudentTypeOptions = _selectListservice.GetStudentTypeSelectList();

        return model;     
    }

    public Student GetStudent(CreateStudentViewModel model)
    {
        Student student = new()
        {
            SchoolId = model.SchoolId,
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            BirthDay = model.BirthDay,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Barangay = model.Barangay,
            Municipality = model.Municipality,
            Province = model.Province,
            YearLevel = model.YearLevel,
            Section = model.Section,
            Sex = model.Sex,
            Type = model.Type,
            DateAdded = DateTime.Now,
            Program = GetProgramForeignKeyAssociation(int.Parse(model.Program)),
            Department = GetDepartmentForeignKeyAssociation(int.Parse(model.Department)),
            Skills = GetRangeSkillsForeignKeyAssociation(model.SkillIds),
            Courses = GetRangeCoursesForeignKeyAssociation(model.CourseIds)
        };

        return student;
    }

    private ICollection<Course> GetRangeCoursesForeignKeyAssociation(List<string> courseIds)
    {
        ICollection<Course> courses = [];

        foreach(var courseId in courseIds)
        {
            var numCourseId = int.Parse(courseId);

            if (_courseRepository.IsExists(numCourseId))
            {
                courses.Add(new Course() {Id = numCourseId});
            }
        }

        return courses;
    }

    private ICollection<Skill> GetRangeSkillsForeignKeyAssociation(List<string> skillIds)
    {
        ICollection<Skill> skills = [];

        foreach (var skillId in skillIds)
        {
            var numSkillId = int.Parse(skillId);

            if (_skillRepository.IsExists(numSkillId))
            {
                skills.Add(new Skill{Id = numSkillId});
            }
        }

        return skills;

    }

    private Department GetDepartmentForeignKeyAssociation(int id)
    {
        if (_departmentRepository.IsExists(id))
        {
            return new Department() {Id = id};
        }

        throw new ArgumentNullException($"Department with an id of {id} can't be found.");
    }

    private SchoolProgram GetProgramForeignKeyAssociation(int id)
    {
        if (_programRepository.IsExists(id))
        {
            return new SchoolProgram() {Id = id};
        }

        throw new ArgumentException($"Program with an id of {id} can't be found.");
    }

    private static DateOnly GetCurrentDateAndTime()
    {
        return DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
    }
}
