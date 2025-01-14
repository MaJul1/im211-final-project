using System;
using System.Collections.Generic;

namespace Populator.Database;

public partial class Skill
{
    public int Id { get; set; }

    public string DateAdded { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
