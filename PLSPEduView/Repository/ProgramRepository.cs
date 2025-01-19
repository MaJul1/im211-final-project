using System;
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

    public IEnumerable<SchoolProgram> GetAll()
    {
        return _context.Programs.AsNoTracking();
    }

    public int GetCount()
    {
        return _context.Programs.Count();
    }

    public void Create(SchoolProgram program)
    {
        _context.Programs.Add(program);
        _context.SaveChanges();
    }

    public SchoolProgram? GetById(int Id)
    {
        return _context.Programs.Find(Id);
    }

    public SchoolProgram? GetByCode(string code)
    {
        return _context.Programs.FirstOrDefault(p => p.Code == code);
    }

    public bool IsExists(int id)
    {
        return _context.Programs.Any(c => c.Id == id);
    }

    public bool Any()
    {
        return _context.Programs.AsNoTracking().Any();
    }

}
