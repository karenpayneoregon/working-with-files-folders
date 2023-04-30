using HelperLibrary;
using Spectre.Console;
using System.Runtime.CompilerServices;

namespace IsFileLockedSample;

internal class Program
{
    static async Task Main(string[] args)
    {
        await TextFileSample();
        await ExcelSample();
    }

    private static async Task TextFileSample()
    {
        var fileName = "TextFile1.txt";
        AnsiConsole.MarkupLine(
            $"[yellow]Attempt to read {fileName} while open with share restricted then a second time with the fileStream disposed [/]");
        var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);

        var result = await FileHelpers.CanReadFile(fileName);
        AnsiConsole.MarkupLine($"Can read {fileName}? [cyan]{result}[/]");
        await fileStream.DisposeAsync();

        result = await FileHelpers.CanReadFile(fileName);
        AnsiConsole.MarkupLine($"Can read {fileName}? [cyan]{result}[/]");

        Console.ReadLine();
    }

    private static async Task ExcelSample()
    {
        string fileName = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "SampleExcelFile",
            "Customers.xlsx");


        AnsiConsole.MarkupLine("[yellow]Open Customers.xlsx then press ENTER[/]");


        Console.ReadLine();
        var result = await FileHelpers.CanReadFile(fileName);
        AnsiConsole.MarkupLine($"Can read Customers.xlsx? [cyan]{result}[/]");

        AnsiConsole.MarkupLine("[yellow]Close Customers.xlsx then press ENTER[/]");
        Console.ReadLine();
        result = await FileHelpers.CanReadFile(fileName);
        AnsiConsole.MarkupLine($"Can read Customers.xlsx? [cyan]{result}[/]");

        Console.ReadLine();
    }

    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        W.SetConsoleWindowPosition(W.AnchorWindow.Center);
    }
}
