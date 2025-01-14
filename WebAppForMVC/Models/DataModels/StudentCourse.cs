using System;

namespace WebAppForMVC.Models.DataModels;

public class StudentCourse
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
}
