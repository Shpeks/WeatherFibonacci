namespace ConsoleRestApp;

public class WeatherFibonacciResult
{
    public string City { get; set; }
    public double TempInCelsius { get; set; }
    public List<int> Fibonacci { get; set; } = new();
}