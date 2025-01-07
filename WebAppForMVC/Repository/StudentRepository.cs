using System;
using WebAppForMVC.Context;

namespace WebAppForMVC.Repository;

public class StudentRepository
{
    private readonly SystemDatabaseContext _context;

    public StudentRepository(SystemDatabaseContext context)
    {
        _context = context;
    }

    public int GetCount()
    {
        return _context.Students.Count();
    }
}
