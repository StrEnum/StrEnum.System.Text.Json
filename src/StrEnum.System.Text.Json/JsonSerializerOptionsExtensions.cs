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
        /// <param name="noMemberFoundBehavior">Specifies whether to throw an exception or use a null value for members that cannot be parsed.</param>
        /// <returns></returns>
        public static JsonSerializerOptions UseStringEnums(this JsonSerializerOptions options, NoMemberFoundBehavior noMemberFoundBehavior = NoMemberFoundBehavior.ThrowException)
        {
            options.Converters.Add(new StringEnumJsonConverterFactory(noMemberFoundBehavior));
            
            return options;
        }
    }
}