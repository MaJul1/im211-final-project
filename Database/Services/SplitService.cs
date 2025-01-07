using System;
using Database.Models;

namespace Database.Services;

public static class SplitService
{
    // '>, <, ==, !=, >=, <=' for filters.
    // ':' for sorting.
    private static readonly string[] _internalComparisonOperators = [":", ">", "<", "==", "!=", ">=", "<="];

    public static SplitData GetSplitData(this string text)
    {
        string[] split = [];

        foreach(var separator in _internalComparisonOperators)
        {
            if (text.Contains(separator))
            {
                split = text.Split(separator);
                break;
            }
        }

        if (split.Length != 2)
        {
            throw new ArgumentException($"'{text}' is invalid argument.");
        }

        return new SplitData()
        {
            Type = split[0].Trim(),
            Value = split[1].Trim()
        };
    }
}
