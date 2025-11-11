using OrderService.Models;

namespace OrderService.Data;

public static class Seed
{
    public static async Task RunAsync(AppDb db)
    {
        if (!db.Orders.Any())
        {
            db.Orders.Add(new Order { Customer = "Alice", Total = 149.90m, Status = "new", CreatedAt = DateTimeOffset.UtcNow.AddDays(-1) });
            db.Orders.Add(new Order { Customer = "Bob", Total = 89.00m, Status = "paid", CreatedAt = DateTimeOffset.UtcNow.AddHours(-4) });
            await db.SaveChangesAsync();
        }
    }
}
