using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForMVC.Models;

namespace WebAppForMVC.Services;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;
    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<SelectListOption> GetSelectListOptionSections()
    {
        var numberOfSection = GetNumberOfSection();

        var chars = GetListOfCharSections(numberOfSection);

        return GetSelectListOption(chars);
    }

    public List<SelectListOption> GetSelectListOptionDepartment()
    {
        var departmentStrings = _configuration.GetSection("Departments").Get<List<string>>();

        return GetSelectListOption(departmentStrings!);
    }

    public List<SelectListOption> GetSelectListOptionProgram()
    {
        var programStrings = _configuration.GetSection("Programs").Get<List<string>>();

        return GetSelectListOption(programStrings!);
    }

    private static List<SelectListOption> GetSelectListOption(List<string> options)
    {
        List<SelectListOption> lists = [];

        foreach(var option in options)
        {
            lists.Add(new() {Value = option, Text = option});
        }

        return lists;
    }
    private static List<string> GetListOfCharSections(int numberSection)
    {
        var list = new List<string>();

        for (int i = 65; i < numberSection + 65; i++)
        {
            list.Add(Convert.ToChar(i).ToString());
        }

        return list;
    }

    private int GetNumberOfSection()
    {
        var value = _configuration["NumberOfSections"];

        var numberSection = int.Parse(value!);

        if (numberSection < 1 || numberSection > 32) throw new ArgumentException($"Number of sections should be between 1 and 32");

        return numberSection;
    }
}
