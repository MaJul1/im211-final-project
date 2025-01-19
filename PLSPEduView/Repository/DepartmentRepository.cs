using System;
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

    public IEnumerable<Department> GetAll()
    {
        return _context.Departments.AsNoTracking();
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

    public Department? GetById(int Id)
    {
        return _context.Departments.Find(Id);
    }

    public Department? GetByCode(string code)
    {
        return _context.Departments.FirstOrDefault(d => d.Code == code);
    }

    public bool IsExists(int id)
    {
        return _context.Departments.Any(c => c.Id == id);
    }

    public bool Any()
    {
        return _context.Departments.AsNoTracking().Any();
    }
}
