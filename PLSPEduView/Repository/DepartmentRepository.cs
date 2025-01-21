using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLSPEduView.Context;
using PLSPEduView.Models;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Services;

namespace PLSPEduView.Repository;

public class DepartmentRepository
{
    private readonly SystemDatabaseContext _context;
    public DepartmentRepository(SystemDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _context.Departments.CountAsync();
    }

    public async Task CreateAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
    }

    public async Task<Department?> GetByIdAsync(int Id)
    {
        return await _context.Departments.FindAsync(Id);
    }

    public async Task<Department?> GetByCodeAsync(string code)
    {
        return await _context.Departments.FirstOrDefaultAsync(d => d.Code == code);
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Departments.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> AnyAsync()
    {
        return await _context.Departments.AsNoTracking().AnyAsync();
    }
}
