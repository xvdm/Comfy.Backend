using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Comfy.Application.Common.Converters;

public class CharacteristicDictionaryConverter : JsonConverter<Dictionary<CharacteristicNameDTO, List<CharacteristicValueDTO>>>
{
    public override Dictionary<CharacteristicNameDTO, List<CharacteristicValueDTO>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //var dictionary = new Dictionary<CharacteristicName, List<CharacteristicValue>>();
        //while (reader.Read())
        //{
        //    if (reader.TokenType == JsonTokenType.EndObject)
        //    {
        //        return dictionary;
        //    }
        //
        //    if (reader.TokenType != JsonTokenType.PropertyName)
        //    {
        //        throw new JsonException();
        //    }
        //
        //    var propertyName = reader.GetString();
        //    var name = JsonSerializer.Deserialize<CharacteristicName>(propertyName, options);
        //
        //    reader.Read();
        //    var values = JsonSerializer.Deserialize<List<CharacteristicValue>>(ref reader, options);
        //
        //    dictionary[name] = values;
        //}
        //
        //throw new JsonException();
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<CharacteristicNameDTO, List<CharacteristicValueDTO>> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        foreach (var pair in value)
        {
            var propertyName = JsonSerializer.Serialize(pair.Key, options);
            writer.WritePropertyName(propertyName);
            JsonSerializer.Serialize(writer, pair.Value, options);
        }
        writer.WriteEndObject();
    }
}