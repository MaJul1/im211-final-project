using System;

namespace PLSPEduView.Models.ViewModels;

public class HomeViewModel
{
    public int NumberOfStudentRegistered {get; set;}
    public int NumberOfSkillsRegistered {get; set;}
    public int NumberOfCoursesRegistered {get; set;}
    public Dictionary<string, int> GroupByProvince = [];
    public Dictionary<string, int> GroupByMunicipality = [];
    public Dictionary<string, int> GroupByYearLevel = [];
    public Dictionary<string, int> GroupBySex = [];
    public Dictionary<string, int> GroupByType = [];
}
