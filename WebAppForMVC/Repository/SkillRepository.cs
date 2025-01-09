using System;
using WebAppForMVC.Context;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Repository;

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
        return _context.Skills;
    }
    
    public void CreateSkill(Skill skill)
    {
        _context.Skills.Add(skill);
        _context.SaveChanges();
    }
}
