namespace WebAppForMVC.Models.DataModels;

public class Course
{
    public int Id {get; set;}
    public string CourseCode {get; set;} = null!;
    public string CourseDescription {get; set;} = null!;
    public int Units {get; set;}
    public DateTime DateAdded{get; set;}

    public ICollection<Student> Students {get; set;} = null!;
    public ICollection<StudentCourse> StudentCourses {get; set;} = null!;

    public int GetNumberOfStudentsEnrolled()
    {
        return Students.Count;
    }
}

