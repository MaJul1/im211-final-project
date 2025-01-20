using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Skill>> GetAllAsync()
    {
        var skill = await _context.Skills.ToListAsync();

        foreach (var s in skill)
        {
            await _context.Entry(s).Collection(s => s.Students).LoadAsync();
        }

        return skill;
    }
    
    public void CreateSkill(Skill skill)
    {
        _context.Skills.Add(skill);
        _context.SaveChanges();
    }

    public async Task<Skill?> GetByIdAsync(int Id)
    {
        var skill = await _context.Skills.FindAsync(Id);

        if (skill == null) return null;

        await _context.Entry(skill).Collection(s => s.Students).LoadAsync();

        foreach (var s in skill.Students)
        {
            await _context.Entry(s).Reference(s => s.Program).LoadAsync();
            await _context.Entry(s).Reference(s => s.Department).LoadAsync();
        }

        return skill;
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Skills.AnyAsync(c => c.Id == id);
    }

    public bool Any()
    {
        return _context.Skills.AsNoTracking().Any();
    }


}
