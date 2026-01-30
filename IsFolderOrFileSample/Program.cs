using System.Runtime.CompilerServices;
using HelperLibrary;
using Spectre.Console;

namespace IsFolderOrFileSample;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> items =
        [
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFile1.txt")
        ];

        foreach (var item in items)
        {
            var (isFolder, success) = FileHelpers.IsFileOrFolder(item);
            if (success)
            {
                AnsiConsole.MarkupLine($"{item}");
                AnsiConsole.MarkupLine($"\tIs folder {isFolder}");
            }
            else
            {
                AnsiConsole.MarkupLine($"[white]{item}[/] [red]not found[/]");
            }
        }

        Console.ReadLine();
    }

    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        W.SetConsoleWindowPosition(W.AnchorWindow.Center);
    }
}
