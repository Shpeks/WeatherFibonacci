using System.Text.Json;

namespace ConsoleRestApp;

public class WeatherFibonacciRest
{
    private readonly HttpClient _httpClient;

    public WeatherFibonacciRest(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }
    /// <summary>
    /// Отправляет асинхронный HTTP-запрос к API для получения информации о погоде и числах Фибоначчи
    /// </summary>
    /// <returns>Объект <see cref="WeatherFibonacciResult"/>, содержащий температуру в городе и соответствующие числа Фибоначчи</returns>
    public async Task<WeatherFibonacciResult> GetAsync(string city)
    {
        var response = await _httpClient.GetAsync($"api/Fibonacci?city={city}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<WeatherFibonacciResult>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}