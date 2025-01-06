using System;
using Database.Enums;

namespace Database.Services;

public record FilterData(string Type, FilterType FilterType, string Value);
public static class FilterSplitService
{
    public static FilterData GetFilterData(this string filter)
    {
        if (filter.Contains('<'))
    }
}
