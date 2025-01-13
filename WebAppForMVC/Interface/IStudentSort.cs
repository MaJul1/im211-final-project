namespace WebAppForMVC.Interface;

public interface IStudentSort
{
    string IdSortParameter {get; set;}
    string NameSortParameter {get; set;}
    string YearAndSectionSortParameter {get; set;}
    string ProgramSortParameter {get; set;}
    string DepartmentSortParameter {get; set;}
    string? SortParameter {get; set;}

}
