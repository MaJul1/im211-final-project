using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForMVC.Models;

namespace WebAppForMVC.Interface;

public interface IStudentOption
{
    List<SelectListOption> SectionOptions {get; set;}
    List<SelectListOption> DepartmentOptions {get; set;}
    List<SelectListOption> ProgramOptions {get; set;}
}
