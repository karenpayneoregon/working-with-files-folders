using UpperFolderExampleApp.Classes;

namespace UpperFolderExampleApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        
        foreach (var parent in AppDomain.CurrentDomain.BaseDirectory.UpperFolders())
        {
            Console.WriteLine(parent);
        }

        SpectreConsoleHelpers.ExitPrompt();
    }
}
