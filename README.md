# StrEnum.System.Text.Json

Lets you serialize and deserialize [StrEnum](https://github.com/StrEnum/StrEnum/) string enums to JSON via System.Text.Json.

Targets .NET Standard 2.0; works with System.Text.Json 4.6.0 – 10.x.

## Installation

Install [StrEnum.System.Text.Json](https://www.nuget.org/packages/StrEnum.System.Text.Json/) via the .NET CLI:

```
dotnet add package StrEnum.System.Text.Json
```

## Usage

### Defining a string enum and a model

```csharp
public class Sport : StringEnum<Sport>
{
    public static readonly Sport RoadCycling = Define("ROAD_CYCLING");
}

public class Race
{
    public string Name { get; set; }
    public Sport Sport { get; set; }
}
```

### Configuring the serializer

Call `UseStringEnums()` on a `JsonSerializerOptions` instance and pass it to `JsonSerializer`:

```csharp
var options = new JsonSerializerOptions().UseStringEnums();
```

### Serializing to JSON

```csharp
var race = new Race { Name = "Cape Town Cycle Tour", Sport = Sport.RoadCycling };

var json = JsonSerializer.Serialize(race, options);
```

Produces:

```json
{"Name":"Cape Town Cycle Tour","Sport":"ROAD_CYCLING"}
```

### Deserializing from JSON

The same JSON can be deserialized back to a `Race`:

```csharp
var race = JsonSerializer.Deserialize<Race>(json, options);

// race is equivalent to:
new { Name = "Cape Town Cycle Tour", Sport = Sport.RoadCycling };
```

## License

Copyright &copy; 2025 [Dmytro Khmara](https://dmytrokhmara.com).

StrEnum is licensed under the [MIT license](LICENSE.txt).
