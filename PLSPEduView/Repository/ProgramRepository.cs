using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLSPEduView.Context;
using PLSPEduView.Models;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Services;

namespace PLSPEduView.Repository;

public class ProgramRepository
{
    private readonly SystemDatabaseContext _context;
    public ProgramRepository(SystemDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SchoolProgram>> GetAllAsync()
    {
        return await _context.Programs.ToListAsync();
    }

    public int GetCount()
    {
        return _context.Programs.Count();
    }

    public async Task CreateAsync(SchoolProgram program)
    {
        await _context.Programs.AddAsync(program);
        await _context.SaveChangesAsync();
    }

    public async Task<SchoolProgram?> GetByIdAsync(int Id)
    {
        return await _context.Programs.FindAsync(Id);
    }

    public async Task<SchoolProgram?> GetByCodeAsync(string code)
    {
        return await _context.Programs.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Programs.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> AnyAsync()
    {
        return await _context.Programs.AsNoTracking().AnyAsync();
    }

}
