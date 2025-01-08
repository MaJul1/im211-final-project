using WebAppForMVC.Models.DataTransferObjects;

namespace WebAppForMVC.Models.ViewModels;

public class StudentViewModel
{
    public int NumberOfStudentsRegistered {get; set;}
    public IEnumerable<StudentProfile> StudentProfiles {get; set;} = null!;
}
