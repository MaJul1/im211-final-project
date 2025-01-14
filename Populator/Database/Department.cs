using System;
using System.Collections.Generic;

namespace Populator.Database;

public partial class Department
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
