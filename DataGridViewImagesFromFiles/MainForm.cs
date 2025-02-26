using System.Diagnostics;
using DataGridViewImagesFromFiles.Classes;
using DataGridViewImagesFromFiles.Models;
using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;

namespace DataGridViewImagesFromFiles;

public partial class MainForm : Form
{
    private static List<FileItem> _files = [];
    public MainForm()
    {
        InitializeComponent();
        Shown += MainForm_Shown;
    }

    private async void MainForm_Shown(object? sender, EventArgs e)
    {
        string[] include = ["**/Ri*.png", "**/Bl*.png"];
        string[] exclude = ["**/blog*.png", "**/black*.png"];
        var folder = "C:\\Users\\paynek\\Documents\\Snagit";

        if (!Directory.Exists(folder))
            return;

        GlobbingOperations.TraverseFileMatch += DirectoryHelpers_TraverseFileMatch;
        GlobbingOperations.Done += DirectoryHelpers_Done;
        await GlobbingOperations.GetImages(folder, include, exclude);

        MatcherParameters matcherParameters = new()
        {
            Patterns = include,
            ExcludePatterns = exclude,
            ParentFolder = folder
        };

        var imageNames = await Globbing.GetImagesNamesAsync(matcherParameters).ConfigureAwait(false);

        foreach (var image in imageNames())
        {
            Debug.WriteLine(image);
        }

        dataGridView1.DataSource = _files;
        dataGridView1.FixHeaders();
        dataGridView1.ExpandColumns();
    }

    private void DirectoryHelpers_TraverseFileMatch(FileMatchItem sender)
    {
        var item = new FileItem() { Folder = sender.Folder, FileName = sender.FileName };
        var fileName = Path.Combine(item.Folder, item.FileName);
        item.Bytes = File.ReadAllBytes(fileName);
        var info = new FileInfo(fileName);
        item.Size = info.Length;
        item.CreationTime = File.GetCreationTime(fileName);
        _files.Add(item);
    }
    private void DirectoryHelpers_Done(string message)
    {
        _files = _files.OrderBy(x => x.FileName).ToList();
    }
}