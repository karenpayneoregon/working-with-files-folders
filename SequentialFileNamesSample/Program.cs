using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using DirectoryHelpersLibrary.Classes;
using SequentialFileNamesSample.Classes;
using Spectre.Console;
using DateOnlyConverter = DirectoryHelpersLibrary.Classes.DateOnlyConverter;
using TimeOnlyConverter = DirectoryHelpersLibrary.Classes.TimeOnlyConverter;


namespace SequentialFileNamesSample
{
    class Program
    {
        static void Main(string[] args)
        {

            //if (GenerateFiles.HasAnyFiles())
            //{
            //    AnsiConsole.MarkupLine($"[cyan]{Path.GetFileName(GenerateFiles.GetLast())}[/]");
            //}

            //AnsiConsole.MarkupLine("[white on blue]Create file[/]");
            //var (success, fileName) = GenerateFiles.CreateTextFile();
            //if (success)
            //{
            //    File.WriteAllLines(fileName, new []{"Hello"});
            //}
            //else
            //{
            //    AnsiConsole.MarkupLine($"[red]Failed to write to[/] {fileName}");
            //}

            //AnsiConsole.MarkupLine($"[cyan]{Path.GetFileName(GenerateFiles.GetLast())}[/]");
            JsonSerializerOptions JsonSerializerOptions()
            {
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);

                jsonSerializerOptions.Converters.Add(new DateOnlyConverter());
                jsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
                jsonSerializerOptions.WriteIndented = true;

                return jsonSerializerOptions;

            }
            JsonSerializerOptions options = JsonSerializerOptions();
            foreach (var person in MockedData.PeopleMocked())
            {
                var (success, fileName) = GenerateFiles.CreateFile();
                File.WriteAllText(fileName, JsonSerializer.Serialize(person, options));

            }

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
