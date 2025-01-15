using System;
using PLSPEduView.Models.DataModels;

namespace PLSPEduView.Models.ViewModels;

public class CourseViewModel
{
    public IEnumerable<Course> Courses {get; set;} = [];
}
