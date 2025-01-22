using System.Threading.Tasks;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class StudentWriteModelService
{
    private readonly SelectListService _selectListservice;
    private readonly SkillRepository _skillRepository;
    private readonly CourseRepository _courseRepository;
    private readonly ProgramRepository _programRepository;
    private readonly DepartmentRepository _departmentRepository;

    public StudentWriteModelService
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
    public async Task<StudentWriteModel> GetStudentWriteModelAsync()
        => await GenerateSelectListsForModel(null);

    public async Task<StudentWriteModel> GetStudentWriteModelAsync(StudentWriteModel model)
        => await GenerateSelectListsForModel(model);

    private async Task<StudentWriteModel> GenerateSelectListsForModel(StudentWriteModel? currentModel)
    {
        StudentWriteModel model;
        
        if (currentModel == null)
        {
            model = new();
        }
        else
        {
            model = currentModel;
        }

        model.BirthDay = await GetCurrentDateAndTimeAsync();

        model.SectionOptions = await _selectListservice.GetSectionSelectListAsync();

        model.CoursesOptions = await _selectListservice.GetCourseSelectListAsync();

        model.YearLevelOptions = await _selectListservice.GetYearLevelSelectListAsync();

        model.SkillOptions = await _selectListservice.GetSkillSelectListAsync();

        model.ProgramOptions = await _selectListservice.GetProgramSelectListAsync();

        model.DepartmentOptions = await _selectListservice.GetDepartmentSelectListAsync();
        
        model.SexOptions = await _selectListservice.GetSexSelectListAsync();

        model.StudentTypeOptions = await _selectListservice.GetStudentTypeSelectListAsync();

        return model;     
    }

    public async Task<StudentWriteModel> GetStudentWriteModelAsync(Student student)
    {
        StudentWriteModel model = new()
        {
            SchoolId = student.SchoolId,
            FirstName = student.FirstName,
            MiddleName = student.MiddleName,
            LastName = student.LastName,
            BirthDay = student.BirthDay,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            Barangay = student.Barangay,
            Municipality = student.Municipality,
            Province = student.Province,
            YearLevel = student.YearLevel,
            Section = student.Section,
            Sex = student.Sex,
            Type = student.Type,
            Program = student.Program.Id.ToString(),
            Department = student.Department.Id.ToString(),
            SkillIds = [.. student.Skills.Select(s => s.Id.ToString())],
            CourseIds = [.. student.Courses.Select(c => c.Id.ToString())]
        };

        model = await GenerateSelectListsForModel(model);

        return model;
    }

    public async Task<Student> GetStudentAsync(StudentWriteModel model)
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
            Program = await GetProgram(int.Parse(model.Program)),
            Department = await GetDepartment(int.Parse(model.Department)),
            Skills = await GetSkills(model.SkillIds),
            Courses = await GetCourses(model.CourseIds)
        };

        return student;
    }

    private async Task<ICollection<Course>> GetCourses(List<string> courseIds)
    {
        ICollection<Course> courses = [];

        foreach(var courseId in courseIds)
        {
            var numCourseId = int.Parse(courseId);

            if (await _courseRepository.IsExistsAsync(numCourseId))
            {
                var course = await _courseRepository.GetByIdAsync(numCourseId);
                courses.Add(course!);
            }
        }

        return courses;
    }

    private async Task<ICollection<Skill>> GetSkills(List<string> skillIds)
    {
        ICollection<Skill> skills = [];

        foreach (var skillId in skillIds)
        {
            var numSkillId = int.Parse(skillId);

            if (await _skillRepository.IsExistsAsync(numSkillId))
            {
                var skill = await _skillRepository.GetByIdAsync(numSkillId);
                skills.Add(skill!);
            }
        }

        return skills;

    }

    private async Task<Department> GetDepartment(int id)
    {
        if (await _departmentRepository.IsExistsAsync(id))
        {
            var department = await _departmentRepository.GetByIdAsync(id)!;

            return department!;
        }

        throw new ArgumentNullException($"Department with an id of {id} can't be found.");
    }

    private async Task<SchoolProgram> GetProgram(int id)
    {
        if (await _programRepository.IsExistsAsync(id))
        {
            var program = await _programRepository.GetByIdAsync(id)!;

            return program!;
        }

        throw new ArgumentException($"Program with an id of {id} can't be found.");
    }

    private static async Task<DateOnly> GetCurrentDateAndTimeAsync()
    {
        return await Task.FromResult(DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
    }
}
