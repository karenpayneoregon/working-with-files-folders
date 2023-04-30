using DirectoryHelpersLibrary.Models;
using Microsoft.Extensions.FileSystemGlobbing;

namespace DirectoryHelpersLibrary.Classes;

public class GlobbingOperations
{
    public delegate void OnTraverseFileMatch(FileMatchItem sender);
    /// <summary>
    /// Informs listener of a <see cref="FileMatchItem"/>
    /// </summary>
    public static event OnTraverseFileMatch TraverseFileMatch;

    public delegate void OnDone(string message);
    /// <summary>
    /// Indicates processing has completed
    /// </summary>
    public static event OnDone Done;

    /// <summary>
    /// Pass back an object which can represent path and file name
    /// </summary>
    /// <param name="parentFolder">folder to start in</param>
    /// <param name="patterns">search include pattern</param>
    /// <param name="excludePatterns">pattern to exclude</param>
    public static async Task Find(string parentFolder, string[] patterns, string[] excludePatterns)
    {

        Matcher matcher = new();
        matcher.AddIncludePatterns(patterns);
        matcher.AddExcludePatterns(excludePatterns);

        await Task.Run( () =>
        {
                
            foreach (string file in matcher.GetResultsInFullPath(parentFolder))
            {
                TraverseFileMatch?.Invoke(new FileMatchItem(file));
            }
        });

        Done?.Invoke("Finished - see log file");

    }


}