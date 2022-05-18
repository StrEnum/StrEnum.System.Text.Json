using System.Text.Json;
using StrEnum.System.Text.Json.Converters;

namespace StrEnum.System.Text.Json
{
    public static class JsonSerializerOptionsExtensions
    {
        /// <summary>
        /// Configure System.Text.Json to serialize and deserialize string enums.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static JsonSerializerOptions UseStringEnums(this JsonSerializerOptions options)
        {
            options.Converters.Add(new StringEnumJsonConverterFactory());
            
            return options;
        }
    }
}