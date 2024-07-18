using System.ComponentModel;
using DataGridViewImagesFromFiles.Classes;
#nullable disable
namespace DataGridViewImagesFromFiles.Models;

public class FileItem
{
    [Browsable(false)]
    public string Folder { get; set; }
    public string FileName { get; set; }
    [Browsable(false)]
    public byte[] Bytes { get; set; }
    public Image Image => Bytes.BytesToImage();
    public long Size { get; set; }
    public DateTime CreationTime { get; set; }
    public override string ToString() => Path.Combine(Folder, FileName);

}