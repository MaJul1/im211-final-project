using Microsoft.EntityFrameworkCore;
using WebAppForMVC.Context;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Repository;

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
            .Include(c => c.Students);
        
        return courses;
    }

    public void CreateCourse(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
    }

    public Course? GetById(Guid Id)
    {
        return _context.Courses.Find(Id);
    }

    public Course? GetByCode(string Code)
    {
        return _context.Courses.FirstOrDefault(c => c.CourseCode == Code);
    }

}
