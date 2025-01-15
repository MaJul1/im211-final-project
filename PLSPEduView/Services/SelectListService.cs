using Microsoft.AspNetCore.Mvc.Rendering;
using PLSPEduView.Models;

namespace PLSPEduView.Services;

public class SelectListService
{
    public static SelectList CreateSelectList(IEnumerable<SelectListOption> options)
    {
        List<SelectListItem> items = [];

        foreach(var s in options)
        {
            items.Add(new SelectListItem() { Value = s.Value, Text = s.Text});
        }

        return new (items, "Value", "Text");
    }

    public static SelectListOption CreateSelectListOption(string value, string text)
    {
        return new SelectListOption() {Value = value, Text = text};
    }
}
