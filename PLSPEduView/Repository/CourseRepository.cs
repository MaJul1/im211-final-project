using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLSPEduView.Context;
using PLSPEduView.Models.DataModels;

namespace PLSPEduView.Repository;

public class CourseRepository
{
    private readonly SystemDatabaseContext _context;

    public CourseRepository(SystemDatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Courses.CountAsync();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        var courses = await _context.Courses
            .Include(c => c.Students)
            .ToListAsync();
        
        return courses;
    }

    public async Task CreateCourseAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }

    public async Task<Course?> GetByIdAsync(int Id)
    {
        var course =  await _context.Courses.FindAsync(Id);

        if (course == null)
        {
            return null;
        }

        await _context.Entry(course).Collection(c => c.Students).LoadAsync();

        foreach (var s in course.Students)
        {
            await _context.Entry(s).Reference(s => s.Program).LoadAsync();
            await _context.Entry(s).Reference(s => s.Department).LoadAsync();
        }

        return course;
    }

    public async Task<Course?> GetByCodeAsync(string Code)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.CourseCode == Code);
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Courses.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> AnyAsync()
    {
        return await _context.Courses.AsNoTracking().AnyAsync();
    }

    public async Task<bool> IsCodeExistsAsync(string code)
    {
        return await _context.Courses.AnyAsync(c => c.CourseCode == code);
    }

    public async Task Remove(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course != null)
        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();
    }
}
