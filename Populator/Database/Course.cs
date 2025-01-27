using System;
using System.Collections.Generic;

namespace Populator.Database;

public partial class Course
{
    public int Id { get; set; }

    public string CourseCode { get; set; } = null!;

    public string CourseDescription { get; set; } = null!;

    public int Units { get; set; }

    public DateTime DateAdded { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
