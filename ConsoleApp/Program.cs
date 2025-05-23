﻿using Core;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var conf = ConfigurationLoader.LoadConfiguration();

var services = new ServiceCollection();

services.Configure<OpenWeatherMapApiOptions>(
    conf.GetSection("OpenWeatherMapApiOptions"));

services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(conf["ConnectionStrings:DefaultConnection"]));
    
services.AddScoped<IFibonacciService, FibonacciService>();
services.AddScoped<IWeatherFibonacciService, WeatherFibonacciService>();

services.AddHttpClient<IWeatherService, WeatherService>((serviceProvider, client) =>
{
    var options = serviceProvider
        .GetRequiredService<IOptions<OpenWeatherMapApiOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);
});

var serviceProvider = services.BuildServiceProvider();

var weatherFibonacciService = serviceProvider.GetRequiredService<IWeatherFibonacciService>();

// Цикл пользовательского ввода с выводом погоды и чисел Фибоначчи
while (true)
{
    Console.WriteLine("Введите название города (или 'exit' для выхода): ");
    var city = Console.ReadLine();
    
    if(city.Trim().ToLower() == "exit")
    {
        Console.WriteLine("Завершение работы...");
        break;
    }
    if (string.IsNullOrWhiteSpace(city))
    {
        Console.WriteLine("Город не может быть пустым. Попробуйте снова.\n");
        continue;
    }
    try
    {
        var weatherFibonacciData = await weatherFibonacciService.GetWeatherFibonacciAsync(city);

        Console.WriteLine($"\nВ городе '{weatherFibonacciData.City}': {weatherFibonacciData.Temperature} C");
        Console.WriteLine("Числа Фибоначчи: " + string.Join(", ", weatherFibonacciData.Fibonacci));
    }
    catch (Exception e)
    {
        Console.WriteLine($"Ошибка: {e.Message}");
    }
}
