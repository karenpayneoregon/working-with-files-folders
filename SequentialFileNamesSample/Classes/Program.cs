using System.Text.Json;
using DirectoryHelpersLibrary.Classes;

// ReSharper disable once CheckNamespace
namespace SequentialFileNamesSample;
internal partial class Program
{
    static JsonSerializerOptions JsonSerializerOptions()
    {
        JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.General);

        jsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        jsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
        jsonSerializerOptions.WriteIndented = true;

        return jsonSerializerOptions;

    }
}
