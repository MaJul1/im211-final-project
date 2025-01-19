using System.Reflection.Metadata.Ecma335;
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

    public IEnumerable<Course> GetAll()
    {
        var courses = _context.Courses
            .Include(c => c.Students)
            .AsNoTracking();
        
        return courses;
    }

    public void CreateCourse(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
    }

    public Course? GetById(int Id)
    {
        var course =  _context.Courses.Find(Id);

        if (course == null)
        {
            return null;
        }

        _context.Entry(course).Collection(c => c.Students).Load();

        foreach (var s in course.Students)
        {
            _context.Entry(s).Reference(s => s.Program).Load();
            _context.Entry(s).Reference(s => s.Department).Load();
        }

        return course;
    }

    public Course? GetByCode(string Code)
    {
        return _context.Courses.FirstOrDefault(c => c.CourseCode == Code);
    }

    public bool IsExists(int id)
    {
        return _context.Courses.Any(c => c.Id == id);
    }

    public bool Any()
    {
        return _context.Courses.AsNoTracking().Any();
    }
}
