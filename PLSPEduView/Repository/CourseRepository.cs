using System.Reflection.Metadata.Ecma335;
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

    public int GetCount()
    {
        return _context.Courses.Count();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        var courses = await _context.Courses
            .Include(c => c.Students)
            .ToListAsync();
        
        return courses;
    }

    public void CreateCourse(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
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

    public Course? GetByCode(string Code)
    {
        return _context.Courses.FirstOrDefault(c => c.CourseCode == Code);
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Courses.AnyAsync(c => c.Id == id);
    }

    public bool Any()
    {
        return _context.Courses.AsNoTracking().Any();
    }
}
