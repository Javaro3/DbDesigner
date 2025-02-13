using System.Text;

namespace Common.Extensions;

public static class StringExtension
{
    public static string ConvertToPascalCase(this string input)
    {
        input = char.ToUpper(input[0]) + input[1..];
        if (string.IsNullOrEmpty(input))
            return input;
        
        if (!IsPascalCase(input))
            return input;
        
        char[] delimiters = ['_', '-', ':', ' '];
        var parts = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        var result = new StringBuilder();
        
        foreach (var part in parts)
        {
            if (part.Length > 0)
            {
                result.Append(char.ToUpper(part[0]));
                result.Append(part.Substring(1).ToLower());
            }
        }

        return result.ToString();
    }
    
    public static string ToPlural(this string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return word;

        if (word.EndsWith("s", StringComparison.OrdinalIgnoreCase) ||
            word.EndsWith("x", StringComparison.OrdinalIgnoreCase) ||
            word.EndsWith("z", StringComparison.OrdinalIgnoreCase) ||
            word.EndsWith("ch", StringComparison.OrdinalIgnoreCase) ||
            word.EndsWith("sh", StringComparison.OrdinalIgnoreCase))
            return word + "es";

        if (word.EndsWith("y", StringComparison.OrdinalIgnoreCase) && word.Length > 1 && !IsVowel(word[^2]))
            return word[..^1] + "ies";
        if (word.EndsWith("f", StringComparison.OrdinalIgnoreCase))
            return word[..^1] + "ves";

        if (word.EndsWith("fe", StringComparison.OrdinalIgnoreCase))
            return word[..^2] + "ves";

        return word + "s";
    }

    private static bool IsVowel(char c) => "aeiou".Contains(char.ToLower(c));
    
    private static bool IsPascalCase(string input)
    {
        var words = System.Text.RegularExpressions.Regex.Split(input, "(?<!^)(?=[A-Z])");

        foreach (var word in words)
        {
            if (string.IsNullOrEmpty(word) || !char.IsUpper(word[0]) || word.Substring(1).Any(char.IsUpper))
                return false;
        }

        return true;
    }
}