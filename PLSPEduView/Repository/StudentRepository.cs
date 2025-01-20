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

    public int GetCount()
    {
        return _context.Students.Count();
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

    public void RemoveById(string Id)
    {
        var student = _context.Students.Find(Id);

        if (student != null) _context.Students.Remove(student);

        _context.SaveChanges();
    }
    
    public bool Any()
    {
        return _context.Students.AsNoTracking().Any();
    }
}
