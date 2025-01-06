using System;
using Database.Models;
using Database.Options;
using Microsoft.Extensions.Logging;

namespace Database.Services.Command.Query;

public class StudentQueryService
{
    public IQueryable<Student> ApplyStudentQuery(IQueryable<Student> students, QueryOptions options)
    {
        if (options.Filter != null)
            students = ApplyStudentFilters(students, options.Filter);
        
        if (options.Sort != null)
            students = ApplyStudentSorts(students, options.Sort);
        
        if (options.GroupBy != null)
            students = ApplyStudentGroupBy(students, options.GroupBy);
        
        return students;
    }

    private IQueryable<Student> ApplyStudentGroupBy(IQueryable<Student> students, string groupBy)
    {
        throw new NotImplementedException();
    }

    private IQueryable<Student> ApplyStudentSorts(IQueryable<Student> students, IEnumerable<string> sort)
    {
        throw new NotImplementedException();
    }

    private IQueryable<Student> ApplyStudentFilters(IQueryable<Student> students, IEnumerable<string> filters)
    {
        foreach (var filter in filters)
        {
            students = ApplyStudentFilter(students, filter);
        }

        return students;
    }

    private IQueryable<Student> ApplyStudentFilter(IQueryable<Student> students, string filter)
    {
        if (filter.Contains('>'))
        {
            var split = filter.Split('>');
            var type = split[0].Trim();
            var value = split[1].Trim();
            students = ApplyStudentGreaterThanFilter(students, type, value);

        }
    }

    private IQueryable<Student> ApplyStudentGreaterThanFilter(IQueryable<Student> students, string type, string value)
    {
        throw new NotImplementedException();
    }
}
