using PLSPEduView.Context;
using PLSPEduView.Models.DataModels;

namespace PLSPEduView.Repository;

public class SkillRepository
{
    private readonly SystemDatabaseContext _context;

    public SkillRepository(SystemDatabaseContext context)
    {
        _context = context;
    }

    public int GetCount()
    {
        return _context.Skills.Count();
    }

    public IEnumerable<Skill> GetAll()
    {
        var skill = _context.Skills;

        foreach (var s in skill)
        {
            _context.Entry(s).Collection(s => s.Students).Load();
        }

        return skill;
    }
    
    public void CreateSkill(Skill skill)
    {
        _context.Skills.Add(skill);
        _context.SaveChanges();
    }

    public Skill? GetById(int Id)
    {
        var skill =  _context.Skills.Find(Id);

        if (skill == null) return null;

        _context.Entry(skill).Collection(s => s.Students).Load();

        foreach (var s in skill.Students)
        {
            _context.Entry(s).Reference(s => s.Program).Load();
            _context.Entry(s).Reference(s => s.Department).Load();
        }

        return skill;
    }

    public bool IsExists(int id)
    {
        return _context.Skills.Any(c => c.Id == id);
    }


}
