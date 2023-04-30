using System.Text.RegularExpressions;

namespace SequentialFileNamesSample.Classes;
public static class StringExtensions
{
    public static int SqueezeInt(this string sender) 
        => int.Parse(Regex.Match(sender, @"\d+").Value);
}
