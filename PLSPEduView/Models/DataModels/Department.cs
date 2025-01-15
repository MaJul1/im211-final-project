using System;
using System.Text.Json.Serialization;

namespace PLSPEduView.Models.DataModels;

public partial class Department
{
    public int Id {get; set;}
    public string Code {get; set;} = null!;
    public string Description {get; set;} = null!;

    [JsonIgnore]
    public ICollection<Student> Students {get; set;} = null!;

    public string GetFullText()
    {
        return string.Join(" - ", Code, Description);
    }
}
