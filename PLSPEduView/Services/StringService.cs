using System;

namespace PLSPEduView.Services;

public static class StringService
{
    public static string ToCapitalized(this string word)
    {
        if (string.IsNullOrEmpty(word) == false)
        {
            word = word.ToLower();
            var chars = word.ToCharArray();
            chars[0] = chars[0].ToString().ToUpper()[0];
            word = string.Concat(chars);
        }

        return word;
    }
}
