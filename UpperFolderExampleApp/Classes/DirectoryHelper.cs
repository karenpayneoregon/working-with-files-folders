namespace UpperFolderExampleApp.Classes;

public static class DirectoryHelper
{
    /// <summary>
    /// Retrieves all parent directories of the specified folder, starting from its immediate parent
    /// and moving up the directory hierarchy.
    /// </summary>
    /// <param name="folderName">
    /// The full path of the folder whose parent directories are to be retrieved. 
    /// If <c>null</c> or empty, no directories are returned.
    /// </param>
    /// <returns>
    /// An enumerable collection of strings representing the full paths of the parent directories.
    /// </returns>
    public static IEnumerable<string> UpperFolders(this string? folderName)
    {
        if (string.IsNullOrWhiteSpace(folderName))
            yield break;

        var directory = new DirectoryInfo(folderName);

        while (directory.Parent != null)
        {
            directory = directory.Parent;
            yield return directory.FullName;
        }
    }

}
