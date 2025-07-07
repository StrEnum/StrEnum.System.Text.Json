# StrEnum.System.Text.Json

Allows for [StrEnum](https://github.com/StrEnum/StrEnum/) string enum JSON serialization and deserialization with System.Text.Json.

The package targets .NET Standard 2.0 and can be used with System.Text.Json 4.6.0-9.\*.

## Installation

You can install [StrEnum.System.Text.Json](https://www.nuget.org/packages/StrEnum.System.Text.Json/) using the .NET CLI:

```
dotnet add package StrEnum.System.Text.Json
```

## Usage

Create a string enum and a class that contains it:

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

Configure `JsonSerializerOptions` by calling the `UseStringEnums()` method and pass it to `JsonSerializer` :

```csharp
var options = new JsonSerializerOptions().UseStringEnums();
```

### Serialize to JSON:

```csharp
var ctct = new Race { Name = "Cape Town Cycle Tour", Sport = Sport.RoadCycling };

var json = JsonSerializer.Serialize(ctct, options);
```

The above produces:

```json
{"Name":"Cape Town Cycle Tour","Sport":"ROAD_CYCLING"}
```

### Deserialize from JSON:

```json
{"Name":"Cape Town Cycle Tour","Sport":"ROAD_CYCLING"}
```

The above JSON can be deserialized into a C# object that contains a StrEnum enum:

```csharp
var race = JsonSerializer.Deserialize<Race>(json, options);

// race is equivalent to:
new { Name = "Cape Town Cycle Tour", Sport = Sport.RoadCycling };
```

## License

Copyright &copy; 2025 [Dmytro Khmara](https://dmytrokhmara.com).

StrEnum is licensed under the [MIT license](LICENSE.txt).