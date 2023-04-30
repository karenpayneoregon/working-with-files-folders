using System.Runtime.CompilerServices;
using System.Text.Json;
using DirectoryHelpersLibrary.Classes;
using SequentialFileNamesSample.Classes;
using Spectre.Console;


namespace SequentialFileNamesSample
{
    partial class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.MarkupLine(GenerateFiles.HasAnyFiles()
                ? $"Last file [cyan]{Path.GetFileName(GenerateFiles.GetLast())}[/]"
                : "[cyan]No files yet[/]");

            JsonSerializerOptions options = JsonSerializerOptions();
            AnsiConsole.MarkupLine("[white on blue]Create files[/]");
            foreach (var person in MockedData.PeopleMocked())
            {
                var (success, fileName) = GenerateFiles.CreateFile();
                if (success)
                {
                    AnsiConsole.MarkupLine($"   [white]{Path.GetFileName(fileName)}[/]");
                    File.WriteAllText(fileName, JsonSerializer.Serialize(person, options));
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]Failed to create {fileName}[/]");
                }
                

            }

            Console.WriteLine();
            AnsiConsole.MarkupLine($"[white]Next file to be created[/] {Path.GetFileName(GenerateFiles.NextFileName())}");

            AnsiConsole.MarkupLine("[cyan]Done[/]");
            Console.ReadLine();
        }



        [ModuleInitializer]
        public static void Init()
        {
            Console.Title = "Filename Code sample";
            W.SetConsoleWindowPosition(W.AnchorWindow.Center);
        }

    }
}
