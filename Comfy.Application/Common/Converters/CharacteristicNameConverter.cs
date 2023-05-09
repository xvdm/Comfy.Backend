using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Comfy.Application.Common.Converters;

public class CharacteristicNameConverter : JsonConverter<CharacteristicNameDTO>
{
    public override CharacteristicNameDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, CharacteristicNameDTO value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        //writer.WriteNumber("Id", value.Id);
        //writer.WriteString("Name", value.Name);

        writer.WriteNumberValue(value.Id);
        writer.WriteStringValue(value.Name);

        writer.WriteEndObject();
    }
}