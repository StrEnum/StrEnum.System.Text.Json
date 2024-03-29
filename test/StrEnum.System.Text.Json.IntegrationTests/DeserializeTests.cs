using System.Text.Json;
using FluentAssertions;
using StrEnum.System.Text.Json.Converters;
using Xunit;

namespace StrEnum.System.Text.Json.IntegrationTests
{
    public class DeserializeTests
    {
        public class Sport : StringEnum<Sport>
        {
            public static readonly Sport TrailRunning = Define("TRAIL_RUNNING");
        }

        public class DeserializedObject
        {
            public Sport? Sport { get; set; }
        }

        [Fact]
        public void Deserialize_GivenJsonWithEnumsValue_ShouldDeserializesItWithValidEnumMember()
        {
            var json = @"{""sport"":""TRAIL_RUNNING""}";

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                .UseStringEnums();

            var obj = JsonSerializer.Deserialize<DeserializedObject>(json, options);

            obj.Should().BeEquivalentTo(new { Sport = Sport.TrailRunning });
        }

        [Fact]
        public void Deserialize_GivenJsonWithEnumsName_AndDefaultNoMemberBehavior_ShouldThrowAnException()
        {
            var json = @"{""sport"":""TrailRunning""}";

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                .UseStringEnums();

            var deserialize = () => JsonSerializer.Deserialize<DeserializedObject>(json, options);

            deserialize.Should().Throw<JsonException>()
                .WithMessage("Requested value 'TrailRunning' was not found in the string enum 'Sport'.");
        }

        [Fact]
        public void Deserialize_GivenJsonWithNull_ShouldDeserializesItWithNull()
        {
            var json = @"{""sport"":null}";

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                .UseStringEnums();

            var obj = JsonSerializer.Deserialize<DeserializedObject>(json, options);

            obj.Should().BeEquivalentTo(new { Sport = (Sport?)null });
        }

        [Fact]
        public void Deserialize_GivenJsonWithInvalidValue_AndDefaultNoMemberBehavior_ShouldThrowAnException()
        {
            var json = @"{""sport"":""Quidditch""}";

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                .UseStringEnums();

            var deserialize = () => JsonSerializer.Deserialize<DeserializedObject>(json, options);

            deserialize.Should().Throw<JsonException>()
                .WithMessage("Requested value 'Quidditch' was not found in the string enum 'Sport'.");
        }

        [Fact]
        public void Deserialize_GivenJsonWithInvalidValue_AndReturnNullNoMemberBehavior_ShouldReturnNull()
        {
            var json = @"{""sport"":""Quidditch""}";

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                .UseStringEnums(NoMemberFoundBehavior.ReturnNull);

            var obj = JsonSerializer.Deserialize<DeserializedObject>(json, options);

            obj.Should().BeEquivalentTo(new { Sport = (Sport?)null });
        }

        [Fact]
        public void Deserialize_GivenJsonWithValidName_AndReturnNullNoMemberBehavior_ShouldReturnNull()
        {
            var json = @"{""sport"":""TrailRunning""}";

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                .UseStringEnums(NoMemberFoundBehavior.ReturnNull);

            var obj = JsonSerializer.Deserialize<DeserializedObject>(json, options);

            obj.Should().BeEquivalentTo(new { Sport = (Sport?)null });
        }
    }
}