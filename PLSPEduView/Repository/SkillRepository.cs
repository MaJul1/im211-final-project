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

    public async Task<int> GetCountAsync()
    {
        return await _context.Skills.CountAsync();
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
    
    public async Task CreateSkillAsync(Skill skill)
    {
        await _context.Skills.AddAsync(skill);
        await _context.SaveChangesAsync();
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

    public async Task<bool> AnyAsync()
    {
        return await _context.Skills.AsNoTracking().AnyAsync();
    }


}
