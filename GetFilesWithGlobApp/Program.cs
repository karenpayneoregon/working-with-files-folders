using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;

namespace GetFilesWithGlobApp;

internal partial class Program
{
    private static List<FileParts> _files = new();
    static async Task Main(string[] args)
    {
        // what to include in the search
        string[] include = ["**/Date*.cs", "**/Time*.cs", "**/ex*.cs", "**/*.md"];
        // exclude these from the search
        string[] exclude =
        [
            "**/*lic*.md",
            "**/*.AssemblyInfo.cs",
            "**/*.AssemblyAttributes.cs",
            "**/*.RazorAssemblyInfo.cs",
            "**/*.g.cs"
        ];

        GlobbingOperations.TraverseFileMatch += DirectoryHelpers_TraverseFileMatch;
        GlobbingOperations.Done += DirectoryHelpers_Done;

        var solutionFolder = DirectoryHelper.GetSolutionInfo().FullName;
        await GlobbingOperations.Find(solutionFolder, include, exclude);
        AnsiConsole.MarkupLine("[yellow]Hello[/]");
        Console.ReadLine();
    }
    private static void DirectoryHelpers_Done(string message)
    {
        _files = _files.OrderBy(x => x.FileName).ToList();
        foreach (var file in _files)
        {
            Console.WriteLine(file);
        }
    }

    private static void DirectoryHelpers_TraverseFileMatch(FileMatchItem sender)
    {
        _files.Add(new FileParts() { Folder = sender.Folder, FileName = sender.FileName });
    }
}