using System.Runtime.CompilerServices;
using SequentialFileNamesSample.Classes;
using Spectre.Console;

namespace SequentialFileNamesSample
{
    class Program
    {
        static void Main(string[] args)
        {

            
            if (!string.IsNullOrWhiteSpace(Path.GetFileName(Operations.GetLast())))
            {
                AnsiConsole.MarkupLine($"[cyan]{Path.GetFileName(Operations.GetLast())}[/]");
            }

            AnsiConsole.MarkupLine("[white on blue]Create 4 files[/]");
            Run();
            AnsiConsole.MarkupLine("[white on blue]Create 8 files[/]");
            Run(8);

            // get last file
            AnsiConsole.MarkupLine($"[cyan]{Path.GetFileName(Operations.GetLast())}[/]");

            Console.ReadLine();
        }

        private static void Run(int count = 4)
        {
            for (int index = 0; index < count; index++)
            {
                File.WriteAllText(Operations.NextFileName(), "");
            }

            Directory.GetFiles(".", "*.txt")
                .ToList()
                .Select(item => new { FileName = Path.GetFileName(item), Index = item.SqueezeInt() })
                .OrderBy(anonymous => anonymous.Index)
                .ToList()
                .ForEach(x => Console.WriteLine(x.FileName));
        }

        [ModuleInitializer]
        public static void Init()
        {
            Console.Title = "Filename Code sample";
            W.SetConsoleWindowPosition(W.AnchorWindow.Center);
        }

    }
}
