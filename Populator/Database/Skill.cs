using System;
using System.Collections.Generic;

namespace Populator.Database;

public partial class Skill
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateAdded { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
