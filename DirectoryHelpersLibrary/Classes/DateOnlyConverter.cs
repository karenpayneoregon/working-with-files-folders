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