using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DirectoryHelpersLibrary.Classes;

namespace SequentialFileNamesSample;
internal partial class Program
{
    static JsonSerializerOptions JsonSerializerOptions()
    {
        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);

        jsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        jsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
        jsonSerializerOptions.WriteIndented = true;

        return jsonSerializerOptions;

    }
}
