using System;
using Database.Models;
using Database.Options;
using Database.Services.Command.Query;

namespace Database.Services.Command;

public static class QueryServices
{
    private static StudentQueryService _studentQueryService = new();
    public static IQueryable<Student> ApplyQuery(this IQueryable<Student> students, QueryOptions options)
    {
        return _studentQueryService.ApplyStudentQuery(students, options);
    }
}
