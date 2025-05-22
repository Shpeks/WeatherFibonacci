using Core.Interfaces;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class FibonacciService : IFibonacciService
{
    private readonly ApplicationDbContext _context;

    public FibonacciService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<int>> GetFibonacciNumberAsync(int count)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = GenerateFibonacciNum(count).ToList();
            
            var entity = new FibonacciNum
            {
                Input = count,
                Output = string.Join(",", result)
            };
            
            _context.FibonacciNums.Add(entity);
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private IEnumerable<int> GenerateFibonacciNum(int count)
    {
        var result = new List<int>();
        int a = 0;
        int b = 1;
        
        for (int i = 0; i < count; i++)
        {
            result.Add(a);
            
            int next = a + b;
            a = b;
            b = next;
        }
        
        return result;
    }
}