using System.Threading.Tasks;
using PLSPEduView.Enums;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class SeederService
{
    private readonly StudentRepository _studentRepository;
    private readonly CourseRepository _courseRepository;
    private readonly SkillRepository _skillRepository;
    private readonly ProgramRepository _programRepository;
    private readonly DepartmentRepository _departmentRepository;

    public SeederService
    (
        StudentRepository studentRepository,
        CourseRepository courseRepository,
        SkillRepository skillRepository,
        ProgramRepository programRepository,
        DepartmentRepository departmentRepository
    )
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _skillRepository = skillRepository;
        _programRepository = programRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task SeedDataAsync()
    {

        if (await _courseRepository.AnyAsync() == false)
        {
            foreach (var c in await GetCoursesAsync())
            {
                await _courseRepository.CreateCourseAsync(c);
            }
        }

        if ( await _skillRepository.AnyAsync() == false)
        {
            foreach (var s in await GetSkillsAsync())
            {
                await _skillRepository.CreateSkillAsync(s);
            }
        }

        if (await _programRepository.AnyAsync() == false)
        {
            foreach (var p in await GetProgramsAsync())
            {
                await _programRepository.CreateAsync(p);
            }
        }

        if (await _departmentRepository.AnyAsync() == false)
        {
            foreach (var d in await GetDepartmentsAsync())
            {
                await _departmentRepository.CreateAsync(d);
            }
        }

        if (await _studentRepository.AnyAsync() == false)
        {
            foreach (var s in await GetListOfStudentsAsync())
            {
                await _studentRepository.CreateStudentAsync(s);
            }
        }

    }

    private async Task<List<Student>> GetListOfStudentsAsync()
    {
        var im211Course = await _courseRepository.GetByCodeAsync("IM211");
        var cs101Course = await _courseRepository.GetByCodeAsync("IM211");
        var chkDepartment = await _departmentRepository.GetByCodeAsync("CHK");
        var ccstDepartment = await _departmentRepository.GetByCodeAsync("CCST");
        var bsitProgram = await _programRepository.GetByCodeAsync("BSIT");
        var bpedProgram = await _programRepository.GetByCodeAsync("BPED");

        List<Student> students = new List<Student>
        {
            new()
            {
                SchoolId = "23-78563",
                FirstName = "Mark",
                MiddleName = "Stance",
                LastName = "Craig",
                BirthDay = new DateOnly(2000, 12, 25),
                Email = "craig@example.com",
                PhoneNumber = "09387465231",
                YearLevel = 1,
                Section = 'B',
                Sex = SexType.MALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now,
                Barangay = "Santa Ana",
                Municipality = "Laguna",
                Province = "Laguna",
                Department = chkDepartment!,
                Program = bpedProgram!,
                Courses = [im211Course!, cs101Course!]
            },
            new()
            {
                SchoolId = "23-37645",
                FirstName = "Jane",
                MiddleName = "Doe",
                LastName = "Smith",
                BirthDay = new DateOnly(2001, 5, 15),
                Email = "jane.smith@example.com",
                PhoneNumber = "09123456789",
                YearLevel = 2,
                Section = 'A',
                Sex = SexType.FEMALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now,
                Barangay = "San Juan",
                Municipality = "Quezon City",
                Province = "Metro Manila",
                Department = ccstDepartment!,
                Program = bsitProgram!,
                Courses = [im211Course!, cs101Course!]
            },
            new()
            {
                SchoolId = "23-11223",
                FirstName = "John",
                MiddleName = "Michael",
                LastName = "Doe",
                BirthDay = new DateOnly(1999, 8, 10),
                Email = "john.doe@example.com",
                PhoneNumber = "09876543210",
                YearLevel = 3,
                Section = 'C',
                Sex = SexType.MALE,
                Type = StudentType.IRREGULAR,
                DateAdded = DateTime.Now,
                Barangay = "Santa Cruz",
                Municipality = "Manila",
                Province = "Metro Manila",
                Department = ccstDepartment!,
                Program = bsitProgram!,
                Courses = [im211Course!, cs101Course!]
            },
            new()
            {
                SchoolId = "23-44567",
                FirstName = "Alice",
                MiddleName = "Marie",
                LastName = "Johnson",
                BirthDay = new DateOnly(2002, 3, 12),
                Email = "alice.johnson@example.com",
                PhoneNumber = "09234567890",
                YearLevel = 2,
                Section = 'D',
                Sex = SexType.FEMALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now,
                Barangay = "San Miguel",
                Municipality = "Pasig",
                Province = "Metro Manila",
                Department = ccstDepartment!,
                Program = bsitProgram!,
                Courses = [im211Course!, cs101Course!]
            },
            new()
            {
                SchoolId = "23-55678",
                FirstName = "Bob",
                MiddleName = "Arthur",
                LastName = "Brown",
                BirthDay = new DateOnly(2000, 7, 22),
                Email = "bob.brown@example.com",
                PhoneNumber = "09198765432",
                YearLevel = 4,
                Section = 'E',
                Sex = SexType.MALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now,
                Barangay = "San Andres",
                Municipality = "Cavite City",
                Province = "Cavite",
                Department = chkDepartment!,
                Program = bpedProgram!,
                Courses = [im211Course!, cs101Course!]
            }
        };

        return students;
    }

    private static async Task<List<Course>> GetCoursesAsync()
    {
        List<Course> courses = new List<Course>
        {
            new()
            {
                CourseCode = "IM211",
                CourseDescription = "Fundamental of Database System",
                Units = 3
            },
            new()
            {
                CourseCode = "CS101",
                CourseDescription = "Introduction to Computer Science",
                Units = 3
            },
            new()
            {
                CourseCode = "CS102",
                CourseDescription = "Data Structures and Algorithms",
                Units = 3
            },
            new()
            {
                CourseCode = "CS103",
                CourseDescription = "Operating Systems",
                Units = 3
            },
            new()
            {
                CourseCode = "CS104",
                CourseDescription = "Computer Networks",
                Units = 3
            },
            new()
            {
                CourseCode = "CS105",
                CourseDescription = "Software Engineering",
                Units = 3
            }
        };

        return await Task.FromResult(courses);
    }

    private static async Task<List<Skill>> GetSkillsAsync()
    {
        List<Skill> skills =
        [
            new()
            {
                Description = "Programming",
            },
            new()
            {
                Description= "Database Management",
            },
            new()
            {
                Description= "Web Development",
            },
            new()
            {
                Description = "Project Management",
            },
            new()
            {
                Description = "Networking",
            },
            new()
            {
                Description = "Cybersecurity",
            }
        ];

        return await Task.FromResult(skills);
    }

    private static async Task<List<Department>> GetDepartmentsAsync()
    {
        List<Department> departments = new()
        {
            new () { Code = "CCST", Description = "College of Computer Studies and Technology" },
            new () { Code = "CHK", Description = "College of Human Kinetics" },
            new () { Code = "COA", Description = "College of Accountancy" },
            new () { Code = "CAHS", Description = "College Of Allied Health Science" },
            new () { Code = "CAS", Description = "College of Arts and Science" },
            new () { Code = "CBA", Description = "College of Business Administration" },
            new () { Code = "COE", Description = "College of Engineering" },
            new () { Code = "CTHM", Description = "College of Tourism And Hospitality Management" },
            new () { Code = "CTEd", Description = "Bachelor of Teacher Education" }
        };
        return await Task.FromResult(departments);
    }

    private static async Task<List<SchoolProgram>> GetProgramsAsync()
    {
        List<SchoolProgram> program = new()
        {
            new () { Code = "BSIT", Description = "Bachelor of Science in Information Technology" },
            new () { Code = "BSIS", Description = "Bachelor of Science in Information System" },
            new () { Code = "BSPED", Description = "Bachelor of Science in Physical Education" },
            new () { Code = "BSA", Description = "Bachelor of Science in Accountancy" },
            new () { Code = "BSAIS", Description = "Bachelor of Science in Accounting Information System" },
            new () { Code = "BSMA", Description = "Bachelor Of Science In Management Accounting" },
            new () { Code = "BSN", Description = "Bachelor of Science in Nursing" },
            new () { Code = "BAPS", Description = "Bachelor of Arts in Political Science" },
            new () { Code = "BAC", Description = "Bachelor of Arts in Communication" },
            new () { Code = "BPA", Description = "Bachelor of Public Administration" },
            new () { Code = "BSP", Description = "Bachelor of Science in Psychology" },
            new () { Code = "BSEc", Description = "Bachelor of Science in Economics" },
            new () { Code = "BSBA", Description = "Bachelor of Science in Business Administration" },
            new () { Code = "BSEnt", Description = "Bachelor of Science in Entrepreneurship" },
            new () { Code = "BSOA", Description = "Bachelor of Science Office Administration" },
            new () { Code = "BSCE", Description = "Bachelor of Science in Computer Engineering" },
            new () { Code = "BSHM", Description = "Bachelor of Science in Hospitality Management" },
            new () { Code = "BSTM", Description = "Bachelor of Science in Tourism Management" },
            new () { Code = "BSNEd", Description = "Bachelor of Special Need Education" },
            new () { Code = "BTTVTEd", Description = "Bachelor of Technical - Vocational Teacher Education" },
            new () { Code = "BECEd", Description = "Bachelor of Early Childhood Education" },
            new () { Code = "BEEd", Description = "Bachelor of Elementary Education" },
            new () { Code = "BPEd", Description = "Bachelor of Physical Education" },
            new () { Code = "BSEd", Description = "Bachelor of Secondary Education" }        
        };
        return await Task.FromResult(program);
    }
}
