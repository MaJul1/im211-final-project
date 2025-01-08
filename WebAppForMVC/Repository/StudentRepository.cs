using System;
using Microsoft.EntityFrameworkCore;
using WebAppForMVC.Context;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Repository;

public class StudentRepository
{
    private readonly SystemDatabaseContext _context;

    public StudentRepository(SystemDatabaseContext context)
    {
        _context = context;
    }

    public int GetCount()
    {
        return _context.Students.Count();
    }

    public IEnumerable<Student> GetAll()
    {
        return _context.Students;
    }

    public async Task CreateStudent(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public Student? GetStudentById(Guid Id)
    {
        var students = _context.Students
            .Include(s => s.Courses)
            .Include(s => s.Skills);

        var student= students.Where(s => s.Id == Id).FirstOrDefault();

        return student;
    }

    public bool Exist(Guid Id)
    {
        var student = _context.Students.Find(Id);

        if (student == null)
            return false;

        return true;
    }
}
