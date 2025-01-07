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
        
        return students;
    }

    public IQueryable<IGrouping<object, Student>> ApplyStudentGroupBy(IQueryable<Student> students, IEnumerable<string> properties)
    {
        return students.GroupByDynamic(properties);
    }

    private IQueryable<Student> ApplyStudentSorts(IQueryable<Student> students, IEnumerable<string> sorts)
    {
        foreach (var sort in sorts)
        {
            students = ApplyStudentSort(students, sort);
        }

        return students;
    }

    private IQueryable<Student> ApplyStudentSort(IQueryable<Student> students, string sort)
    {
        var splitData = sort.GetSplitData();
        var sortType = splitData.Value.ParseToSortType();
        var type = splitData.Type;

        students = students.ApplyFieldSort(type, s => s.Id, sortType);
        students = students.ApplyFieldSort(type, s => s.FirstName, sortType);
        students = students.ApplyFieldSort(type, s => s.MiddleName, sortType);
        students = students.ApplyFieldSort(type, s => s.LastName, sortType);
        students = students.ApplyFieldSort(type, s => s.Age, sortType);
        students = students.ApplyFieldSort(type, s => s.BirthDay, sortType);
        students = students.ApplyFieldSort(type, s => s.Email, sortType);
        students = students.ApplyFieldSort(type, s => s.PhoneNumber, sortType);
        students = students.ApplyFieldSort(type, s => s.Address, sortType);
        students = students.ApplyFieldSort(type, s => s.Department, sortType);
        students = students.ApplyFieldSort(type, s => s.YearLevel, sortType);
        students = students.ApplyFieldSort(type, s => s.Section, sortType);
        students = students.ApplyFieldSort(type, s => s.Program, sortType);
        students = students.ApplyFieldSort(type, s => s.Sex, sortType);
        students = students.ApplyFieldSort(type, s => s.Type, sortType);

        return students;
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
