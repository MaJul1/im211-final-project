using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForMVC.Models;

namespace WebAppForMVC.Services;

public class SelectListService
{
    public static SelectList GetSelectList(IEnumerable<SelectListOption> options)
    {
        List<SelectListItem> items = [];

        foreach(var s in options)
        {
            items.Add(new SelectListItem() { Value = s.Value, Text = s.Text});
        }

        return new (items, "Value", "Text");
    }
}
