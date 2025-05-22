namespace DAL.Entities;

public class FibonacciNum
{
    public int Id { get; set; }
    
    public int Input { get; set; }

    public string Output { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}