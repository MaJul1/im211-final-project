using Database.InternalData;
using Database.Models;
using Database.Options;

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
        var splitData = filter.GetSplitData();

        if (filter.Contains('>'))
            students = ApplyStudentGreaterThanFilter(students, splitData);
        else if (filter.Contains('<'))
            students = ApplyStudentLessThanFilter(students, splitData);
        else if (filter.Contains(">="))
            students = ApplyStudentGreaterThanOrEqualFilter(students, splitData);
        else if (filter.Contains("<="))
            students = ApplyStudentLessThanOrEqualFilter(students, splitData);
        else if (filter.Contains("=="))
            students = ApplyStudentEqualFilter(students, splitData);
        else if (filter.Contains("!="))
            students = ApplyStudentNotEqualFilter(students,splitData);

        return students;
    }

    private static IQueryable<Student> ApplyStudentNotEqualFilter(IQueryable<Student> students, SplitData splitData)
    {
        students = students.ApplyFieldFilter(splitData.Type, s => s.Id != splitData.Value.ParseToGuid());
        students = students.ApplyFieldFilter(splitData.Type, s => s.FirstName != splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.MiddleName != splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.LastName != splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.BirthDay != splitData.Value.ParseToExactDateOnly("yyyy-MM-dd"));
        students = students.ApplyFieldFilter(splitData.Type, s => s.Age != splitData.Value.ParseToNumber());
        students = students.ApplyFieldFilter(splitData.Type, s => s.Email != splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.PhoneNumber != splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.Address != splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.Department != splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.YearLevel != splitData.Value.ParseToNumber());
        students = students.ApplyFieldFilter(splitData.Type, s => s.Section != splitData.Value.ParseToChar());
        students = students.ApplyFieldFilter(splitData.Type, s => s.Program != splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.Sex != splitData.Value.ParseToSex());
        students = students.ApplyFieldFilter(splitData.Type, s => s.Type != splitData.Value.ParseToStudentType());

        return students;
    }

    private static IQueryable<Student> ApplyStudentEqualFilter(IQueryable<Student> students, SplitData splitData)
    {
        students = students.ApplyFieldFilter(splitData.Type, s => s.Id == splitData.Value.ParseToGuid());
        students = students.ApplyFieldFilter(splitData.Type, s => s.FirstName == splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.MiddleName == splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.LastName == splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.BirthDay == splitData.Value.ParseToExactDateOnly("yyyy-MM-dd"));
        students = students.ApplyFieldFilter(splitData.Type, s => s.Age == splitData.Value.ParseToNumber());
        students = students.ApplyFieldFilter(splitData.Type, s => s.Email == splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.PhoneNumber == splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.Address == splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.Department == splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.YearLevel == splitData.Value.ParseToNumber());
        students = students.ApplyFieldFilter(splitData.Type, s => s.Section == splitData.Value.ParseToChar());
        students = students.ApplyFieldFilter(splitData.Type, s => s.Program == splitData.Value);
        students = students.ApplyFieldFilter(splitData.Type, s => s.Sex == splitData.Value.ParseToSex());
        students = students.ApplyFieldFilter(splitData.Type, s => s.Type == splitData.Value.ParseToStudentType());

        return students;
    }

    private static IQueryable<Student> ApplyStudentLessThanOrEqualFilter(IQueryable<Student> students, SplitData splitData)
    {
        students = students.ApplyFieldFilter(splitData.Type, s => s.BirthDay <= splitData.Value.ParseToExactDateOnly("yyyy-MM-dd"));
        students = students.ApplyFieldFilter(splitData.Type, s => s.Age <= splitData.Value.ParseToNumber());
        students = students.ApplyFieldFilter(splitData.Type, s => s.YearLevel <= splitData.Value.ParseToNumber());

        return students;
    }

    private static IQueryable<Student> ApplyStudentGreaterThanOrEqualFilter(IQueryable<Student> students, SplitData splitData)
    {
        students = students.ApplyFieldFilter(splitData.Type, s => s.BirthDay >= splitData.Value.ParseToExactDateOnly("yyyy-MM-dd"));
        students = students.ApplyFieldFilter(splitData.Type, s => s.Age >= splitData.Value.ParseToNumber());
        students = students.ApplyFieldFilter(splitData.Type, s => s.YearLevel >= splitData.Value.ParseToNumber());

        return students;
    }

    private static IQueryable<Student> ApplyStudentLessThanFilter(IQueryable<Student> students, SplitData splitData)
    {
        students = students.ApplyFieldFilter(splitData.Type, s => s.BirthDay < splitData.Value.ParseToExactDateOnly("yyyy-MM-dd"));
        students = students.ApplyFieldFilter(splitData.Type, s => s.Age < splitData.Value.ParseToNumber());
        students = students.ApplyFieldFilter(splitData.Type, s => s.YearLevel < splitData.Value.ParseToNumber());

        return students;
    }

    private static IQueryable<Student> ApplyStudentGreaterThanFilter(IQueryable<Student> students, SplitData splitData)
    {
        students = students.ApplyFieldFilter(splitData.Type, s => s.BirthDay > splitData.Value.ParseToExactDateOnly("yyyy-MM-dd"));
        students = students.ApplyFieldFilter(splitData.Type, s => s.Age > splitData.Value.ParseToNumber());
        students = students.ApplyFieldFilter(splitData.Type, s => s.YearLevel > splitData.Value.ParseToNumber());

        return students;
    }
}
