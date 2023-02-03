namespace API;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    // '?' indicates it's optional. this is new to .NET6. it's called a nullable property. check out the .csproj files for nullable flag.
    // we set Nullable to disable, and we removed the '?' from below
    public string Summary { get; set; }
}
