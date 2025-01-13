using WebAppForMVC.Interface;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Services;

public static class SortService
{
    public static IEnumerable<Student> ApplySort(this IEnumerable<Student> students, ISortOption sort)
    {
        if (sort.IsDescending)
        {
            sort.SortParameter = string.Concat(sort.SortParameter, "_desc");
        }

        switch (sort.SortParameter)
        {
            case "Id":
                students = students.OrderBy(s => s.SchoolId);
                break;
            case "Id_desc":
                students = students.OrderBy(s => s.Id);
                break;
            case "Name":
                students = students.OrderBy(s => s.FirstName).ThenBy(s => s.LastName);
                break;
            case "Name_desc":
                students = students.OrderByDescending(s => s.FirstName).ThenBy(s => s.LastName);
                break;
            case "YearAndSection":
                students = students.OrderBy(s => s.YearLevel).ThenBy(s => s.Section);
                break;
            case "YearAndSection_desc":
                students = students.OrderByDescending(s => s.YearLevel).ThenBy(s => s.Section);
                break;
            case "Program":
                students = students.OrderBy(s => s.Program.Code);
                break;
            case "Program_desc":
                students = students.OrderByDescending(s => s.Program.Code);
                break;
            case "Department":
                students = students.OrderBy(s => s.Department.Code);
                break;
            case "Department_desc":
                students = students.OrderByDescending(s => s.Department.Code);
                break;
        }

        return students;
    }
}
