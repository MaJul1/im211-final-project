using System;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Models.ViewModels;

public class CourseViewModel
{
    public IEnumerable<Course> Courses {get; set;} = [];
}
