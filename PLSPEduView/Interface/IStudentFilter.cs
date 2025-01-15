using PLSPEduView.Enums;

namespace PLSPEduView.Interface;

public interface IStudentFilter
{
    int? AgeMinimumFilter {get; set;}
    int? AgeMaximumFilter {get; set;}
    string? MunicipalityFilter {get; set;}
    string? ProvinceFilter {get; set;}
    int? YearLevelFilter {get; set;}
    char? SectionFilter {get; set;}
    string? ProgramFilter {get; set;}
    string? DepartmentFilter {get; set;}
    SexType? SexFilter {get; set;}
    StudentType? TypeFilter {get; set;}
    string? SearchParameter {get; set;}
}
