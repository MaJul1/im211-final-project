using WebAppForMVC.Models.DataTransferObjects;

namespace WebAppForMVC.Models.ViewModels;

public class StudentViewModel
{
    public int NumberOfStudentsRegistered {get; set;}
    public List<StudentProfile> StudentProfiles {get; set;} = null!;
}
