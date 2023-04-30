using System.Runtime.CompilerServices;
using DirectoryHelpersLibrary.Classes;
using Spectre.Console;

namespace SequentialFileNamesSample
{
    class Program
    {
        static void Main(string[] args)
        {

            // if there are any files show the last one created
            if (GenerateFiles.HasAnyFiles())
            {
                AnsiConsole.MarkupLine($"[cyan]{Path.GetFileName(GenerateFiles.GetLast())}[/]");
            }

            AnsiConsole.MarkupLine("[white on blue]Create 1 file[/]");
            GenerateFiles.Create();
            AnsiConsole.MarkupLine("[white on blue]Create 4 files[/]");
            GenerateFiles.Create(4);

            // get last file
            AnsiConsole.MarkupLine($"[cyan]{Path.GetFileName(GenerateFiles.GetLast())}[/]");

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
