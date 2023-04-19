using Comfy.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Comfy.Persistence.Converters;

public class CharacteristicDictionaryConverter : JsonConverter<Dictionary<CharacteristicName, List<CharacteristicValue>>>
{
    public override Dictionary<CharacteristicName, List<CharacteristicValue>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

    public override void Write(Utf8JsonWriter writer, Dictionary<CharacteristicName, List<CharacteristicValue>> value, JsonSerializerOptions options)
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