using Comfy.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Comfy.Persistence.Converters;

public class CharacteristicNameConverter : JsonConverter<CharacteristicName>
{
    public override CharacteristicName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, CharacteristicName value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        //writer.WriteNumber("Id", value.Id);
        //writer.WriteString("Name", value.Name);

        writer.WriteNumberValue(value.Id);
        writer.WriteStringValue(value.Name);

        writer.WriteEndObject();
    }
}