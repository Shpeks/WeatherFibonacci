using Core.Models;

namespace Core.Services;

public interface IWeatherFibonacciService
{
    Task<WeatherFibonacciData> GetWeatherFibonacciAsync(string city);
}