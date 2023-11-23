# Example for Globbing.

```csharp
// what to include in the search
string[] include = { "**/Date*.cs", "**/Time*.cs", "**/ex*.cs", "**/*.md" };
// exclude these from the search
string[] exclude =
{
    "**/*lic*.md",
    "**/*.AssemblyInfo.cs",
    "**/*.AssemblyAttributes.cs",
    "**/*.RazorAssemblyInfo.cs",
    "**/*.g.cs"
};
```

## In the above

- **include** array are the glob pattern for searching for in the current solution folder
    - First element, any file starting with **Date** with **.cs** extension
    - Second element, any file starting with **Time** with **.cs** extension
    - Third element, and file that starts with **ex** with **.cs** extension
    - Fourth element, all markdown files (*see exclude array below*)
- **exclude** array are the glob patterns to exclude from the search.
- First element, exclude markdown file starting with Lic and not since we have ***.md** in the include array all other markdown files are included
- Second, third and fourth are files that we don't care about.

## DirectoryHelpersLibrary

Contains all core code for the above.
