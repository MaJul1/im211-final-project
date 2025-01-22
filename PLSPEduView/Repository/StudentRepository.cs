using System.Threading.Tasks;
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

    public async Task<int> GetCountAsync()
    {
        return await _context.Students.CountAsync();
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task CreateStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        
        await _context.SaveChangesAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int Id)
    {
        var student = await _context.Students
                .AsSplitQuery()
                .FirstOrDefaultAsync(s => s.Id == Id);

        if(student != null)
        {
            await _context.Entry(student).Collection(s => s.Courses).LoadAsync();
            await _context.Entry(student).Collection(s => s.Skills).LoadAsync();
            await _context.Entry(student).Reference(s => s.Program).LoadAsync();
            await _context.Entry(student).Reference(s => s.Department).LoadAsync();
        }

        return student;
    }

    public async Task<bool> ExistAsync(int Id)
    {
        var student = await _context.Students.FindAsync(Id);

        return student != null;
    }

    public async Task RemoveByIdAsync(string Id)
    {
        var student = await _context.Students.FindAsync(Id);

        if (student != null) _context.Students.Remove(student);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Student newStudent, int id)
    {
        var existingStudent = await _context.Students.FindAsync(id);

        if (existingStudent != null)
        {
            newStudent.Id = existingStudent.Id;

            _context.Entry(existingStudent).CurrentValues.SetValues(newStudent);

            existingStudent.Courses = newStudent.Courses;

            existingStudent.Skills = newStudent.Skills;

            existingStudent.Program = newStudent.Program;

            existingStudent.Department = newStudent.Department;

            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<bool> AnyAsync()
    {
        return await _context.Students.AsNoTracking().AnyAsync();
    }
}
