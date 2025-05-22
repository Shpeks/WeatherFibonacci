namespace Core.Models;

public class WeatherData
{
    public MainWeather Main { get; set; }
}

public class MainWeather
{
    public string City { get; set; }
    public double Temp { get; set; }
}