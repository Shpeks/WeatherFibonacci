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
    /// <summary>
    /// Обрабатывает gRPC-запрос на получение температуры и чисел Фибоначчи по названию города
    /// </summary>
    /// <param name="request">gRPC-запрос, содержащий название города</param>
    /// <param name="context">Контекст gRPC-сервера</param>
    /// <returns>Ответ <see cref="WeatherResponse"/> с температурой и числами Фибоначчи</returns>
    /// <exception cref="RpcException">Выбрасывается при отсутствии названия города или при внутренних ошибках сервера</exception>
    public override async Task<WeatherResponse> GetWeatherAndFibonacci(
        WeatherRequest request,
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
                Temperature = weatherFibonacciData.Temperature,
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