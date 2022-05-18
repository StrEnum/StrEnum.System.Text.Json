using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrEnum.System.Text.Json.Converters;

internal class StringEnumJsonConverterFactory : JsonConverterFactory
{
    private readonly ConcurrentDictionary<Type, JsonConverter> _converters = new();

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsStringEnum();
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (_converters.TryGetValue(typeToConvert, out var cachedConverter))
        {
            return cachedConverter;
        }

        return _converters.GetOrAdd(typeToConvert, BuildConverter);
    }

    private JsonConverter BuildConverter(Type stringEnum)
    {
        var converterType = typeof(StringEnumJsonConverter<>).MakeGenericType(stringEnum);

        var converter = Activator.CreateInstance(converterType) as JsonConverter;

        return converter!;
    }
}