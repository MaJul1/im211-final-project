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

    public int GetCount()
    {
        return _context.Departments.Count();
    }

    public void Create(Department department)
    {
        _context.Departments.Add(department);
        _context.SaveChanges();
    }

    public async Task<Department?> GetByIdAsync(int Id)
    {
        return await _context.Departments.FindAsync(Id);
    }

    public Department? GetByCode(string code)
    {
        return _context.Departments.FirstOrDefault(d => d.Code == code);
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Departments.AnyAsync(c => c.Id == id);
    }

    public bool Any()
    {
        return _context.Departments.AsNoTracking().Any();
    }
}
