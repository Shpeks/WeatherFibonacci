namespace Core.Interfaces;

public interface IFibonacciService
{
    Task<IEnumerable<int>> GetFibonacciNumberAsync(int count);
}