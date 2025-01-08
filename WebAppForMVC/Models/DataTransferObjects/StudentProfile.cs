using System;

namespace WebAppForMVC.Models.DataTransferObjects;

public class StudentProfile
{
    public Guid Id {get; set;}
    public string FullName {get; set;} = null!;
    public string YearAndSection {get; set;} = null!;
    public string Program {get; set;} = null!;
    public string Department {get; set;} = null!;
}
