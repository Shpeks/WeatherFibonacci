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
        try
        {
            if (string.IsNullOrWhiteSpace(request.City))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Название города не может быть пустым"));
            }

            var weatherFibonacciData = await _weatherFibonacciService.GetWeatherFibonacciAsync(request.City);

            return new WeatherResponse
            {
                City = weatherFibonacciData.City,
                Temperature = Math.Round(weatherFibonacciData.Temperature - 273.15, 1),
                FibonacciNumbers = { weatherFibonacciData.Fibonacci }
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(
                new Status(StatusCode.Internal, $"Внутренняя ошибка сервера: {ex.Message}"));
        }
    }
}