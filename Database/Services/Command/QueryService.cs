using System;
using ConsoleTables;
using Database.Models;
using Database.Options;
using Database.Services.Command.Query;

namespace Database.Services.Command;

public static class QueryServices
{
    private static StudentQueryService _studentQueryService = new();
    public static void ExecuteQuery(this IQueryable<Student> students, QueryOptions options)
    {
        var newStudents = _studentQueryService.ApplyStudentQuery(students, options);

        if (options.GroupBy != null)
        {
            var groupByResult = _studentQueryService.ApplyStudentGroupBy(newStudents, options.GroupBy);
            ConsoleTable.From((IEnumerable<IQueryable<IGrouping<object, Student>>>)groupByResult)
                .Write();
        }
    }
}
