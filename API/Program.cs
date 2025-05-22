using Core.Interfaces;
using Core.Models;
using Core.Services;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var conf = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllers();

services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(conf.GetConnectionString("DefaultConnection")));

services.AddScoped<IFibonacciService, FibonacciService>();
services.AddScoped<IWeatherFibonacciService, WeatherFibonacciService>();
services.AddHttpClient<IWeatherService, WeatherService>();

services.Configure<OpenWeatherMapApiOptions>(
    builder.Configuration.GetSection("OpenWeatherMapApiOptions"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); 

app.Run();