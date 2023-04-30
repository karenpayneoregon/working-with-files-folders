using System.Text;
using DirectoryHelpersLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using DirectoryHelpersLibrary.Classes;
using Microsoft.AspNetCore.Mvc;

namespace GlobbingRazorProject.Pages;
public class IndexModel : PageModel
{
    public IndexModel()
    {
        GlobbingOperations.TraverseFileMatch += TraverseFileMatch;
    }

    [BindProperty]
    public StringBuilder Builder { get; set; } = new();
    public void OnGet()
    {

    }

    public async Task<PageResult> OnPost()
    {
        string path = DirectoryHelper.SolutionFolder();

        string[] include = { "**/*.cs", "**/*.cshtml" };
        string[] exclude =
        {
            "**/*Assembly*.cs",
            "**/*_*.cshtml",
            "**/*Designer*.cs",
            "**/*.g.i.cs",
            "**/*.g.cs",
            "**/TemporaryGeneratedFile*.cs"
        };

        Builder = new StringBuilder();
        await GlobbingOperations.Find(path, include, exclude);
        return Page();
    }

    private void TraverseFileMatch(FileMatchItem sender)
    {
        Log.Information(Path.Combine(sender.Folder, sender.FileName));
        Builder.AppendLine(Path.Combine(sender.Folder, sender.FileName));
    }
}
