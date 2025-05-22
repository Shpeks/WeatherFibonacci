namespace ConsoleRestApp;

public class WeatherFibonacciResult
{
    public string City { get; set; }
    public double Temperature { get; set; }
    public List<int> Fibonacci { get; set; } = new();
}