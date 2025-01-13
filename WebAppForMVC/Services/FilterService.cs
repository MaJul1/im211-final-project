using WebAppForMVC.Interface;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Services;

public static class FilterService
{
    public static IEnumerable<Student> ApplyFilter(this IEnumerable<Student> students, IStudentFilter filter)
    {
        if (filter.AgeMinimumFilter != null)
        {
            var presentYear = DateTime.Now.Year;
            students = students.Where(s => (presentYear - s.BirthDay.Year) > filter.AgeMinimumFilter);
        }

        if (filter.AgeMaximumFilter != null)
        {
            var presentYear = DateTime.Now.Year;
            students = students.Where(s => (presentYear - s.BirthDay.Year) > filter.AgeMaximumFilter);
        }

        if (string.IsNullOrEmpty(filter.MunicipalityFilter) == false)
        {
            students = students.Where(s => s.Municipality.Equals(filter.MunicipalityFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (string.IsNullOrEmpty(filter.ProvinceFilter) == false)
        {
            students = students.Where(s => s.Province.Equals(filter.ProvinceFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.YearLevelFilter != null)
        {
            students = students.Where(s => s.YearLevel == filter.YearLevelFilter);
        }

        if (filter.SectionFilter != null)
        {
            students = students.Where(s => s.Section == filter.SectionFilter);
        }

        if (string.IsNullOrEmpty(filter.ProgramFilter) == false)
        {
            students = students.Where(s => s.Program.Equals(filter.ProgramFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (string.IsNullOrEmpty(filter.DepartmentFilter) == false)
        {
            students = students.Where(s => s.Department.Equals(filter.DepartmentFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.SexFilter != null)
        {
            students = students.Where(s => s.Sex == filter.SexFilter);
        }

        if (filter.TypeFilter != null)
        {
            students = students.Where(s => s.Type == filter.TypeFilter);
        }

        if (filter.SearchParameter != null)
        {
            students = students.Where(s =>
                s.SchoolId.Contains(filter.SearchParameter, StringComparison.CurrentCultureIgnoreCase) ||
                string.Join(" ", s.FirstName, s.LastName).Contains(filter.SearchParameter, StringComparison.CurrentCultureIgnoreCase) ||
                string.Concat(s.YearLevel, s.Section).Contains(filter.SearchParameter, StringComparison.CurrentCultureIgnoreCase) ||
                s.Program.Contains(filter.SearchParameter, StringComparison.CurrentCultureIgnoreCase) ||
                s.Department.Contains(filter.SearchParameter, StringComparison.CurrentCultureIgnoreCase));
        }

        return students;

    }
}
