using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class WeatherFibonacciService : IWeatherFibonacciService
{
    private readonly IWeatherService _weatherService;
    private readonly IFibonacciService _fibonacciService;

    public WeatherFibonacciService(IFibonacciService fibonacciService, IWeatherService weatherService)
    {
        _fibonacciService = fibonacciService;
        _weatherService = weatherService;
    }

    public async Task<WeatherFibonacciData> GetWeatherFibonacciAsync(string city)
    {
        var weatherData = await _weatherService.GetWeatherDataAsync(city);
        var cityLenght = city.Replace(" ", "").Length;
        var fibonacciData = await _fibonacciService.GetFibonacciNumberAsync(cityLenght);

        return new WeatherFibonacciData
        {
            City = weatherData.Main.City,
            Temperature = weatherData.Main.Temp,
            Fibonacci = fibonacciData
        };
    }
}