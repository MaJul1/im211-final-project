using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppForMVC.Interface;

public interface IStudentOption
{
    SelectList SectionOptions {get; set;}
    SelectList DepartmentOptions {get; set;}
    SelectList ProgramOptions {get; set;}
}
