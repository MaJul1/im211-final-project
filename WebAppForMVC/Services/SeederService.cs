using System;
using WebAppForMVC.Enums;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

public class SeederService
{
    private readonly StudentRepository _studentRepository;
    private readonly CourseRepository _courseRepository;
    private readonly SkillRepository _skillRepository;

    public SeederService (StudentRepository studentRepository, CourseRepository courseRepository, SkillRepository skillRepository)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _skillRepository = skillRepository;
    }

    public async Task SeedData()
    {
        if (_studentRepository.GetAll().Any() == false)
        {
            foreach (var s in GetListOfStudents())
            {
                await _studentRepository.CreateStudent(s);
            }
        }

        if (_courseRepository.GetAll().Any() == false)
        {
            foreach (var c in GetCourses())
            {
                _courseRepository.CreateCourse(c);
            }
        }

        if (_skillRepository.GetAll().Any() == false)
        {
            foreach (var s in GetSkills())
            {
                _skillRepository.CreateSkill(s);
            }
        }
        
    }

    private static List<Student> GetListOfStudents()
    {
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
                Program = "BSIT",
                Department = "CCST",
                Sex = SexType.MALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now,
                Barangay = "Santa Ana",
                Municipality = "Laguna",
                Province = "Laguna"
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
                Program = "BSCS",
                Department = "CCST",
                Sex = SexType.FEMALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now,
                Barangay = "San Juan",
                Municipality = "Quezon City",
                Province = "Metro Manila"
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
                Program = "BSIT",
                Department = "CCST",
                Sex = SexType.MALE,
                Type = StudentType.IRREGULAR,
                DateAdded = DateTime.Now,
                Barangay = "Santa Cruz",
                Municipality = "Manila",
                Province = "Metro Manila"
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
                Program = "BSCS",
                Department = "CCST",
                Sex = SexType.FEMALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now,
                Barangay = "San Miguel",
                Municipality = "Pasig",
                Province = "Metro Manila"
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
                Program = "BSIT",
                Department = "CCST",
                Sex = SexType.MALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now,
                Barangay = "San Andres",
                Municipality = "Cavite City",
                Province = "Cavite"
            }
        };

        return students;
    }

    private static List<Course> GetCourses()
    {
        List<Course> courses = new List<Course>
        {
            new()
            {
                CourseCode = "IM211",
                CourseDescription = "Fundamental of Database System"
            },
            new()
            {
                CourseCode = "CS101",
                CourseDescription = "Introduction to Computer Science"
            },
            new()
            {
                CourseCode = "CS102",
                CourseDescription = "Data Structures and Algorithms"
            },
            new()
            {
                CourseCode = "CS103",
                CourseDescription = "Operating Systems"
            },
            new()
            {
                CourseCode = "CS104",
                CourseDescription = "Computer Networks"
            },
            new()
            {
                CourseCode = "CS105",
                CourseDescription = "Software Engineering"
            }
        };

        return courses;
    }

    public List<Skill> GetSkills()
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

        return skills;
    }
}
