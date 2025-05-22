using Core.Interfaces;
using Core.Services;
using Fibo.Grpc;
using Grpc.Core;

namespace GrpcService.Services;

public class WeatherFiboService : Fibo.Grpc.WeatherFiboService.WeatherFiboServiceBase
{
    private readonly IWeatherFibonacciService _weatherFibonacciService;

    public WeatherFiboService(IWeatherFibonacciService weatherFibonacciService)
    {
        _weatherFibonacciService = weatherFibonacciService;
    }

    public override async Task<WeatherResponse> GetWeatherAndFibonacci(WeatherRequest request,
        ServerCallContext context)
    {
        var city = request.City;

        var weatherFibonacciData = await _weatherFibonacciService.GetWeatherFibonacciAsync(city);

        return new WeatherResponse
        {
            City = weatherFibonacciData.City,
            Temperature = Math.Round(weatherFibonacciData.Temperature - 273.15, 1),
            FibonacciNumbers = { weatherFibonacciData.Fibonacci }
        };
    }
}