# About

Example to traverse folders using [globbing](https://learn.microsoft.com/en-us/dotnet/core/extensions/file-globbing).

This code samples is for iterating a folder structure with enhanced patterns that allow more than simply filtering on file extensions.

## Sample patterns

| Value        | Description     |
|:------------- |:-------------|
| *.txt|All files with .txt file extension. |
| *.\* | All files with an extension|
| * | All files in top-level directory.|
| .*	| File names beginning with '.'.|
| *word\*| All files with 'word' in the filename.|
| readme.*| All files named 'readme' with any file extension.|
| styles/*.css| All files with extension '.css' in the directory 'styles/'.|
| scripts/*/\*| All files in 'scripts/' or one level of subdirectory under 'scripts/'.|
| images*/*| All files in a folder with name that is or begins with 'images'.|
| **/\*| All files in any subdirectory.|
| dir/**/\*| All files in any subdirectory under 'dir/'.|
| ../shared/*| All files in a diretory named "shared" at the sibling level to the base directory|

In this project the goal is to get all `.cs` file with several exclusions as per the variable `exclude`

`await GlobbingOperations.Find(path, include, exclude);` traverses the path which in this case is the current solution folder and on a match adds the match to a StringBuilder which we return the page and append to the body of the page the results.


> **Note**
> Take time to read Microsoft docs and study this code, especially the patterns.


```csharp
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
```
