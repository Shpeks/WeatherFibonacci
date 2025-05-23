namespace DAL.Entities;

public class FibonacciNum
{
    public int Id { get; set; }
    /// <summary>
    /// Входные данные: число
    /// </summary>
    public int Input { get; set; }
    /// <summary>
    /// Выходные данные: числа фибоначчи
    /// </summary>
    public string Output { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}