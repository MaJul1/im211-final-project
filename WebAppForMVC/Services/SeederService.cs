using System;
using WebAppForMVC.Enums;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

public class SeederService
{
    private readonly StudentRepository _studentRepository;
    public SeederService (StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task SeedData()
    {
        if (_studentRepository.GetAll().Any() == true)
        {
            return;
        }
        
        foreach (var s in GetListOfStudents())
        {
            await _studentRepository.CreateStudent(s);
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
                Age = 24,
                Email = "craig@example.com",
                PhoneNumber = "09387465231",
                Address = "St. sample sample log angeles california",
                YearLevel = 1,
                Section = 'B',
                Program = "BSIT",
                Department = "CCST",
                Sex = SexType.MALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now
            },
            new()
            {
                SchoolId = "23-37645",
                FirstName = "Jane",
                MiddleName = "Doe",
                LastName = "Smith",
                BirthDay = new DateOnly(2001, 5, 15),
                Age = 22,
                Email = "jane.smith@example.com",
                PhoneNumber = "09123456789",
                Address = "123 Main St, Springfield",
                YearLevel = 2,
                Section = 'A',
                Program = "BSCS",
                Department = "CCST",
                Sex = SexType.FEMALE,
                Type = StudentType.REGULAR,
                DateAdded = DateTime.Now
            },
            new()
            {
                SchoolId = "23-11223",
                FirstName = "John",
                MiddleName = "Michael",
                LastName = "Doe",
                BirthDay = new DateOnly(1999, 8, 10),
                Age = 25,
                Email = "john.doe@example.com",
                PhoneNumber = "09876543210",
                Address = "456 Elm St, Metropolis",
                YearLevel = 3,
                Section = 'C',
                Program = "BSIT",
                Department = "CCST",
                Sex = SexType.MALE,
                Type = StudentType.IRREGULAR,
                DateAdded = DateTime.Now
            }
        };

        return students;
    }
}
