using System;
using System.ComponentModel.DataAnnotations;

namespace PLSPEduView.Models.ViewModels;

public class CourseWriteModel
{
    [Required(ErrorMessage = "Code is required.")]
    public string Code {get; set;} = null!;

    [Required(ErrorMessage = "Description is required.")]
    public string Description {get; set;} = null!;

    [Required(ErrorMessage = "Units is required.")]
    public int Units {get; set;}
}
