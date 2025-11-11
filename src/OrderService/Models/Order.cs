using System;

namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string Status { get; set; } = "new";
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        // Neu: Unix-Millis für DB-Seitige Sortierung/Filter (SQLite kann das gut)
        public long CreatedAtUnixMs { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}
