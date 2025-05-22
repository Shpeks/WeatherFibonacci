using ConsoleRestApp;

Console.Write("Введите название города: ");
var city = Console.ReadLine();

var client = new WeatherFibonacciRest("https://localhost:7022");

try
{
    var result = await client.GetAsync(city);
    Console.WriteLine($"Город: {result.City}, Температура: {result.TempInCelsius} C");
    Console.WriteLine("Числа Фибоначчи: " + string.Join(", ", result.Fibonacci));
}
catch (Exception e)
{
    Console.WriteLine("Ошибка: " + e.Message);
}