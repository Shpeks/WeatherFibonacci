using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FibonacciController : ControllerBase
{
    private readonly IWeatherFibonacciService _weatherFibonacciService;

    public FibonacciController(IWeatherFibonacciService weatherFibonacciService)
    {
        _weatherFibonacciService = weatherFibonacciService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string city)
    {
        try
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("Заполните поле названием города");
            }
            var weatherFibonacciData = await _weatherFibonacciService.GetWeatherFibonacciAsync(city);
            var tempInCelsius = Math.Round(weatherFibonacciData.Temperature - 273.15, 1);
            return Ok(new
            {
                weatherFibonacciData.City,
                tempInCelsius,
                weatherFibonacciData.Fibonacci
            });
        }
        catch (Exception e)
        {
            return BadRequest("Ошибка при получении информации. Проверьте название города");
        }
    }
}