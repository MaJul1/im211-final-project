using System;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Models.ViewModels;

public class StudentViewModel
{
    public int NumberOfStudentsRegistered {get; set;}
    public List<StudentProfile> StudentProfiles {get; set;} = null!;
}
