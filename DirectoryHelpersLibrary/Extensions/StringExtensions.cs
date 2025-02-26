using System.Text.RegularExpressions;

namespace DirectoryHelpersLibrary.Extensions;
public static partial class StringExtensions
{
    public static int SqueezeInt(this string sender) 
        => int.Parse(DigitRegex().Match(sender).Value);
    [GeneratedRegex(@"\d+")]
    private static partial Regex DigitRegex();
}
