using System.Runtime.InteropServices;

namespace HelperLibrary;
public class FileHelpers
{
    const int ErrorSharingViolation = 32;
    const int ErrorLockViolation = 33;


    /// <summary>
    /// Determine if file can be read
    /// </summary>
    /// <param name="fileName">File name to check</param>
    /// <returns>true ready else false not ready to use</returns>
    public static async Task<bool> CanReadFile(string fileName)
    {
        static bool IsFileLocked(Exception exception)
        {
            var errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode is ErrorSharingViolation or ErrorLockViolation;
        }

        try
        {
            await using var fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }
        catch (IOException ex)
        {
            if (IsFileLocked(ex))
            {
                return false;
            }
        }

        await Task.Delay(50);

        return true;

    }

    /// <summary>
    /// Provides a sort for file names ending with a number e.g. file1.doc
    /// </summary>
    public class NaturalStringComparer : Comparer<string>
    {

        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string x, string y);

        public override int Compare(string x, string y)
            => StrCmpLogicalW(x, y);
    }

    /// <summary>
    /// Determines if path is a file or folder
    /// </summary>
    /// <param name="path">Item to determine type</param>
    /// <returns> isFolder, true if a folder false otherwise  success, the item was found or not </returns>
    public static (bool isFolder, bool success) IsFileOrFolder(string path)
    {
        try
        {
            var attr = File.GetAttributes(path);
            return attr.HasFlag(FileAttributes.Directory) ? (true, true)! : (false, true)!;
        }
        catch (FileNotFoundException)
        {
            return (false, false);
        }
    }
}
