using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrEnum.System.Text.Json.Converters;

/// <summary>
/// Allows to serialize and deserialize string enums.
/// </summary>
/// <typeparam name="TStringEnum"></typeparam>
public class StringEnumJsonConverter<TStringEnum>: JsonConverter<TStringEnum> where TStringEnum: StringEnum<TStringEnum>, new()
{
    private readonly NoMemberFoundBehavior _noMemberFoundBehavior;

    public StringEnumJsonConverter(NoMemberFoundBehavior noMemberFoundBehavior)
    {
        _noMemberFoundBehavior = noMemberFoundBehavior;
    }
    public override TStringEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonValue = reader.GetString();

        StringEnum<TStringEnum>.TryParse(reader.GetString()!, out var parsed);

        if (parsed == null || !((string)parsed).Equals(jsonValue, StringComparison.InvariantCulture))
        {
            if (_noMemberFoundBehavior == NoMemberFoundBehavior.ReturnNull)
                return null!;

            throw new JsonException($"Requested name or value '{jsonValue}' was not found in the string enum '{typeof(TStringEnum).Name}'.");
        }

        return parsed;
    }

    public override void Write(Utf8JsonWriter writer, TStringEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue((string)value);
    }
}