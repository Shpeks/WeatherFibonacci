using Core.Interfaces;
using Core.Models;
using Core.Services;
using DAL.Data;
using GrpcService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var conf = builder.Configuration;
// Add services to the container.
services.AddGrpc();
services.AddHttpClient<IWeatherService, WeatherService>();
services.AddScoped<IFibonacciService, FibonacciService>();
services.AddScoped<IWeatherFibonacciService, WeatherFibonacciService>();

services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(conf.GetConnectionString("DefaultConnection")));

services.Configure<OpenWeatherMapApiOptions>(
    builder.Configuration.GetSection("OpenWeatherMapApiOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<WeatherFiboService>();
app.MapGet("/", () => "gRPC работает. Используйте gRPC клиент.");

app.Run();