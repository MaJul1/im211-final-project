using System;
using System.Collections.Generic;

namespace Populator.Database;

public partial class Student
{
    public int Id { get; set; }

    public string SchoolId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthDay { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Barangay { get; set; } = null!;

    public string Municipality { get; set; } = null!;

    public string Province { get; set; } = null!;

    public int YearLevel { get; set; }

    public string Section { get; set; } = null!;

    public int Sex { get; set; }

    public int Type { get; set; }

    public DateTime DateAdded { get; set; }

    public int ProgramId { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual SchoolProgram Program { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
