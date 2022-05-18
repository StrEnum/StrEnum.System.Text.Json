using System.Text.Json;
using FluentAssertions;
using Xunit;

namespace StrEnum.System.Text.Json.IntegrationTests
{
    public class SerializeTests
    {
        public class Sport : StringEnum<Sport>
        {
            public static readonly Sport TrailRunning = Define("TRAIL_RUNNING");
        }

        [Fact]
        public void Serialize_GivenAnObjectWithStringEnum_ShouldSerializeItUsingTheEnumsValue()
        {
            var obj = new { Sport = Sport.TrailRunning };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                .UseStringEnums();

            string jsonString = JsonSerializer.Serialize(obj, options);

            jsonString.Should().Be(@"{""sport"":""TRAIL_RUNNING""}");
        }

        [Fact]
        public void Serialize_GivenAnObjectWithNullStringEnum_ShouldSerializeItUsingNull()
        {
            var obj = new { Sport = (Sport?)null};

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                .UseStringEnums();

            string jsonString = JsonSerializer.Serialize(obj, options);

            jsonString.Should().Be(@"{""sport"":null}");
        }
    }
}