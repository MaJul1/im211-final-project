using Microsoft.AspNetCore.Mvc.Rendering;
using PLSPEduView.Models;

namespace PLSPEduView.Services;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;
    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<SelectListOption> GetSelectListOptionSections()
    {
        var chars = GetListOfCharSections();

        return GetSelectListOption(chars);
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
    public List<string> GetListOfCharSections()
    {
        var numberSection = GetNumberOfSection();

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
