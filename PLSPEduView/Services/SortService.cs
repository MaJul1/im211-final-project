using PLSPEduView.Interface;
using PLSPEduView.Models.DataModels;

namespace PLSPEduView.Services;

public static class SortService
{
    public static IEnumerable<Student> ApplySort(this IEnumerable<Student> students, ISortOption sort)
    {
        if (sort.IsDescending)
        {
            sort.SortParameter = string.Concat(sort.SortParameter, "_desc");
        }

        students = students.ApplySort(sort.SortParameter);

        if (string.IsNullOrEmpty(sort.SortParameter) == false && sort.SortParameter.Contains("_desc"))
        {
            sort.SortParameter = sort.SortParameter.Split("_desc")[0];
        }

        return students;
    }

    public static IEnumerable<Student> ApplySort(this IEnumerable <Student> students, string? sortParam)
    {
        switch (sortParam)
        {
            case "Id":
                students = students.OrderBy(s => s.SchoolId);
                break;
            case "Id_desc":
                students = students.OrderByDescending(s => s.SchoolId);
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

    public static IEnumerable<Course> ApplySort(this IEnumerable<Course> courses, string? sortParam)
    {
        switch (sortParam)
        {
            case "Code":
                courses = courses.OrderBy(s => s.CourseCode);
            break;
            case "Code_desc":
                courses = courses.OrderByDescending(s => s.CourseCode);
            break;
            case "Description":
                courses = courses.OrderBy(s => s.CourseDescription);
            break;
            case "Description_desc":
                courses = courses.OrderByDescending (s => s.CourseDescription);
            break;
            case "NumberOfStudents":
                courses = courses.OrderBy (s => s.GetNumberOfStudentsEnrolled());
            break;
            case "NumberOfStudents_desc":
                courses = courses.OrderByDescending (s => s.GetNumberOfStudentsEnrolled());
            break;
        }

        return courses;
    }

    public static IEnumerable<Skill> ApplySort(this IEnumerable<Skill> skills, string sortParam)
    {
        switch (sortParam)
        {
            case "Description":
                skills = skills.OrderBy(s => s.Description);
            break;
            case "Description_desc":
                skills = skills.OrderByDescending(s => s.Description);
            break;
            case "NumberOfStudents":
                skills = skills.OrderBy(s => s.GetStudentCount());
            break;
            case "NumberOfStudents_desc":
                skills = skills.OrderByDescending(s => s.GetStudentCount());
            break;
        }

        return skills;
    }
}
