using System;
using Database.Enums;

namespace Database.Services;

public static class ParseService
{
    public static double ParseToNumber(this string value)
    {
        try
        {
            return double.Parse(value);
        }
        catch(FormatException)
        {
            throw new ArgumentException($"{value} must be a number.");
        }
    }

    public static Sex ParseToSex(this string value)
    {
        try
        {
            return (Sex)Enum.Parse(typeof(Sex), value, true);
        }
        catch(ArgumentNullException)
        {
            throw new ArgumentException($"{nameof(Sex)} must not be empty.");
        }
    }

    public static StudentType ParseToStudentType(this string value)
    {
        try
        {
            return (StudentType)Enum.Parse(typeof(StudentType), value, true);
        }
        catch(ArgumentNullException)
        {
            throw new ArgumentException($"{nameof(StudentType)} must not be empty.");
        }
    }

    public static char ParseToChar(this string value)
    {
        try
        {
            return char.Parse(value);
        }
        catch (FormatException)
        {
            throw new ArgumentException($"{value} is not a valid single character.");
        }
    }

    public static Guid ParseToGuid(this string value)
    {
        try
        {
            return Guid.Parse(value);
        }
        catch (FormatException)
        {
            throw new ArgumentException($"{value} is not a valid Guid.");
        }
    }

    public static DateOnly ParseToExactDateOnly(this string value, string format)
    {
        try
        {
            return DateOnly.ParseExact(value, format);
        }
        catch (FormatException)
        {
            throw new ArgumentNullException($"{value} is not a valid date format. Must use the {format} format.");
        }
    }
}
