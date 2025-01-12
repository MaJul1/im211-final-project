using System;
using WebAppForMVC.Interface;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Services;

public static class FilterService
{
    public static IEnumerable<Student> ApplyFilter(this IEnumerable<Student> students, IStudentFilter filter)
    {
        if (filter.BirthdayMinimumFilter != null)
        {
            students = students.Where(s => s.BirthDay > filter.BirthdayMinimumFilter);
        }
        
        if (filter.BirthdayMaximumFilter != null)
        {
            students = students.Where(s => s.BirthDay < filter.BirthdayMaximumFilter);
        }

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

        if (filter.MunicipalityFilter != null)
        {
            students = students.Where(s => s.Municipality.Equals(filter.MunicipalityFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.ProvinceFilter != null)
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

        if (filter.ProgramFilter != null)
        {
            students = students.Where(s => s.Program.Equals(filter.ProgramFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.DepartmentFilter != null)
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
                s.SchoolId.Contains(filter.SearchParameter) ||
                string.Join(" ", s.FirstName, s.LastName).Contains(filter.SearchParameter) ||
                string.Concat(s.YearLevel, s.Section).Contains(filter.SearchParameter) ||
                s.Program.Contains(filter.SearchParameter) ||
                s.Department.Contains(filter.SearchParameter));
        }

        return students;

    }
}
