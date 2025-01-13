using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForMVC.Context;
using WebAppForMVC.Models;
using WebAppForMVC.Models.DataModels;
using WebAppForMVC.Services;

namespace WebAppForMVC.Repository;

public class DepartmentRepository
{
    private readonly SystemDatabaseContext _context;
    public DepartmentRepository(SystemDatabaseContext context)
    {
        _context = context;
    }

    public IEnumerable<Department> GetAll()
    {
        return _context.Departments;
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

    public Department? GetById(Guid Id)
    {
        return _context.Departments.Find(Id);
    }

    public Department? GetByCode(string code)
    {
        return _context.Departments.FirstOrDefault(d => d.Code == code);
    }

    public List<SelectListOption> GetAsSelectListOptions()
    {
        var departments = _context.Departments;

        var options = new List<SelectListOption>();

        foreach (var d in departments)
        {
            options.Add(SelectListService.CreateSelectListOption(d.Id.ToString(), d.GetFullText()));
        }

        return options;
    }
}
