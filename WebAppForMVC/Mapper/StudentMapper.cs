using System;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Models.DataTransferObjects;

namespace WebAppForMVC.Mapper;

public static class StudentMapper
{
    public static StudentProfile ToStudentProfile (this Student student)
    {
        var studentProfile = new StudentProfile()
        {
            Id = student.Id,
            FullName = string.Join(" ", student.FirstName, student.LastName),
            YearAndSection = string.Join("-", student.YearLevel, student.Section),
            Program = student.Program,
            Department = student.Department
        };

        return studentProfile;
    }
}
