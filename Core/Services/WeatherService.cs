using System.Text.Json;
using Microsoft.Extensions.Options;
using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherMapApiOptions _options;
    
    public WeatherService(HttpClient httpClient, IOptions<OpenWeatherMapApiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }
    
    /// <summary>
    /// Получение данных о погоде по названию города 
    /// </summary>
    /// <param name="city">Город</param>
    /// <returns>Возвращает температуру в F</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<WeatherData> GetWeatherDataAsync(string city)
    {
        if (string.IsNullOrEmpty(city))
            throw new ArgumentException("Пустое поле или неверный город");
        
        var baseUrl = _options.BaseUrl;
        var appId = _options.AppId;
        
        var requestUrl = $"{baseUrl}?q={city}&appid={appId}";
        
        var response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();

        var weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
        if (weatherData == null)
            throw new Exception("Не удалось получить данные о погоде");

        weatherData.Main.City = city;
        
        return weatherData;
    }
}