using Microsoft.EntityFrameworkCore;
using PLSPEduView.Context;
using PLSPEduView.Models.DataModels;

namespace PLSPEduView.Repository;

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

    public void CreateStudent(Student student)
    {
        _context.Students.Add(student);
        
        _context.SaveChanges();
    }

    public async Task CreateStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        
        await _context.SaveChangesAsync();
    }

    public Student? GetStudentById(int Id)
    {
        var student = _context.Students
                .AsSplitQuery()
                .FirstOrDefault(s => s.Id == Id);

        if(student != null)
        {
            _context.Entry(student).Collection(s => s.Courses).Load();
            _context.Entry(student).Collection(s => s.Skills).Load();
            _context.Entry(student).Reference(s => s.Program).Load();
            _context.Entry(student).Reference(s => s.Department).Load();
        }

        return student;
    }

    public bool Exist(int Id)
    {
        var student = _context.Students.Find(Id);

        if (student == null)
            return false;

        return true;
    }

    public void RemoveById(string Id)
    {
        var student = _context.Students.Find(Id);

        if (student != null) _context.Students.Remove(student);

        _context.SaveChanges();
    }
}
