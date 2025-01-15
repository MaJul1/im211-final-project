using PLSPEduView.Models;

namespace PLSPEduView.Interface;

public interface IStudentOption
{
    List<SelectListOption> SectionOptions {get; set;}
    List<SelectListOption> DepartmentOptions {get; set;}
    List<SelectListOption> ProgramOptions {get; set;}
}
