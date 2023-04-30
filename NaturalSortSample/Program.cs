using System.Runtime.CompilerServices;
using Spectre.Console;
using static HelperLibrary.FileHelpers;

namespace NaturalSortSample;

internal class Program
{
    static void Main(string[] args)
    {
        StandardSortSample();
        NaturalSortSample();
        Console.ReadLine();
    }

    private static void NaturalSortSample()
    {
        Print("Natural sort");
        var fileNames = FileNames();

        fileNames.Sort(new NaturalStringComparer());

        foreach (var item in fileNames)
        {
            Console.WriteLine(item);
        }

    }
    private static void StandardSortSample()
    {
        Print("Standard sort");
        var fileNames = FileNames();

        foreach (var item in fileNames)
        {
            Console.WriteLine(item);
        }


    }

    private static List<string> FileNames() =>
        new()
        {
            "Example12.txt", "Example2.txt", "Example3.txt", "Example4.txt",
            "Example5.txt", "Example6.txt", "Example7.txt", "Example8.txt",
            "Example9.txt", "Example10.txt", "Example11.txt", "Example1.txt",
            "Example13.txt", "Example14.txt", "Example15.txt", "Example16.txt",
            "Example17.txt", "Example18.txt", "Example19.txt", "Example20.txt"
        };

    private static void Print(string text)
    {
        AnsiConsole.MarkupLine($"[yellow]{text}[/]");
    }

    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        W.SetConsoleWindowPosition(W.AnchorWindow.Center);
    }
}
