using System;
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
}
