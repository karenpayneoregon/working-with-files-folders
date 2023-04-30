# About

Code sample to demonstrate is an item is either a file or a folder and if neither the method returns false.

> **Note**
> In the project file a folder named LogFiles is created.

```csharp
static void Main(string[] args)
{
    var items = new List<string>
    {
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles"),
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"),
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFile1.txt")
    };

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
}
```