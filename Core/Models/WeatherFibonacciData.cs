namespace Core.Models;

public class WeatherFibonacciData
{
    public string City { get; set; }
    public double Temperature { get; set; }
    public IEnumerable<int> Fibonacci { get; set; }
}