namespace DataGridViewImagesFromFiles.Classes;

public static class Extensions
{
    public static Image BytesToImage(this byte[] bytes)
    {
        var imageData = bytes;
        using MemoryStream ms = new(imageData, 0, imageData.Length);
        ms.Write(imageData, 0, imageData.Length);
        return Image.FromStream(ms, true);
    }
}