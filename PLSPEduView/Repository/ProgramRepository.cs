using System;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        return _context.Programs;
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

    public List<SelectListOption> GetAsSelectListOptions()
    {
        var programs = _context.Programs;

        var options = new List<SelectListOption>();

        foreach(var p in programs)
        {
            options.Add(SelectListService.CreateSelectListOption(p.Id.ToString(), p.GetFullText()));
        }

        return options;
    }

}
