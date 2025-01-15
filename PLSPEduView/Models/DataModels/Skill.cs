namespace PLSPEduView.Models.DataModels;

public class Skill
{
    public int Id {get; set;}
    public string Description {get; set;} = null!;
    public DateTime DateAdded {get; set;}

    public ICollection<Student> Students {get; set;} = null!;
    public ICollection<StudentSkill> StudentSkills {get; set;} = null!;

    public int GetStudentCount()
    {
        return Students.Count;
    }
}
