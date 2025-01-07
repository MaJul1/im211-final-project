using System;
using WebAppForMVC.Context;

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
}
