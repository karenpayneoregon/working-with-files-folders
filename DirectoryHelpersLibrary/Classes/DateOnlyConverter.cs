using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DirectoryHelpersLibrary.Classes;

/// <summary>
/// Responsible for serializing and deserializing models to and from json for <see cref="DateOnly"/>
/// </summary>
public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string _serializationFormat;

    public DateOnlyConverter() : this(null) { }

    public DateOnlyConverter(string? serializationFormat)
    {
        // Note the date format may be different for those using this
        _serializationFormat = serializationFormat ?? "MM/dd/yyyy";
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(_serializationFormat));
}

public class JsonNullableDateOnlyConverter : JsonConverter<DateOnly?>
{
    // Define the date format the data is in
    private const string DateFormat = "yyyy MM dd";

    // This is the deserializer
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var input = reader.GetString();
        if (!string.IsNullOrEmpty(input) && input != "null" && input != "0000 00 00")
            return DateOnly.ParseExact(reader.GetString()!, DateFormat);
        else
        {
            return null;
        }
    }

    // This is the serializer
    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(
                DateFormat, CultureInfo.InvariantCulture));
    }
}