namespace DirectoryHelpersLibrary.Models;

public class FileParts
{
    public string Folder { get; set; }
    public string FileName { get; set; }
    public override string ToString() => Path.Combine(Folder,FileName);

}