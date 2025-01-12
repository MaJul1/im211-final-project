using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppForMVC.Services;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;
    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SelectList GetSelectListSection()
    {
        var value = _configuration["NumberOfSections"];

        var numberSection = int.Parse(value!);

        if (numberSection < 1 || numberSection > 32) throw new ArgumentException($"Number of sections should be between 1 and 32");

        var sections = GetListOfCharSections(numberSection);

        var selectList = GetSelectList(sections);

        return selectList;
    }

    public SelectList GetSelectListDepartment()
    {
        var list = _configuration.GetSection("Departments").Get<List<string>>();

        var selectlist = GetSelectList(list);

        return selectlist;
    }

    public SelectList GetSelectListProgram()
    {
        var list = _configuration.GetSection("Programs").Get<List<string>>();

        var selectList = GetSelectList(list);

        return selectList;
    }

    private static SelectList GetSelectList(List<string>? list)
    {
        var items = new List<SelectListItem>();

        if (list == null) return new(items);

        foreach (var d in list)
        {
            items.Add(new() { Value = d, Text = d});
        }

        return new SelectList(items, "Value", "Text");
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
}
