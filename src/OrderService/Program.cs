using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi; // Für .WithOpenApi()
using OrderService.Data;
using OrderService.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<AppDb>(opt =>
{
    // SQLite-Datei im Arbeitsverzeichnis
    opt.UseSqlite("Data Source=app.db");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Kestrel auf 8080
builder.WebHost.ConfigureKestrel(o => { });

var app = builder.Build();

// Ensure DB created + seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDb>();
    await db.Database.EnsureCreatedAsync();
    await Seed.RunAsync(db);
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }))
   .WithName("Health")
   .WithOpenApi();

var group = app.MapGroup("/orders");

// ---- FIX: In-Memory sortieren (SQLite mag DateTimeOffset nicht in ORDER BY)
group.MapGet("/", async (AppDb db) =>
{
    var list = await db.Orders.AsNoTracking().ToListAsync();
    return Results.Ok(list.OrderByDescending(o => o.CreatedAt));
})
.WithName("GetOrders")
.WithOpenApi();

group.MapGet("/{id:int}", async ([FromRoute] int id, AppDb db) =>
{
    var found = await db.Orders.FindAsync(id);
    return found is null ? Results.NotFound() : Results.Ok(found);
})
.WithName("GetOrderById")
.WithOpenApi();

group.MapPost("/", async ([FromBody] CreateOrderDto dto, AppDb db) =>
{
    if (string.IsNullOrWhiteSpace(dto.Customer))
        return Results.BadRequest(new { error = "Customer is required" });

    var order = new Order
    {
        Customer = dto.Customer.Trim(),
        Total = dto.Total <= 0 ? 0 : dto.Total,
        Status = string.IsNullOrWhiteSpace(dto.Status) ? "new" : dto.Status.Trim(),
        CreatedAt = DateTimeOffset.UtcNow
    };
    db.Orders.Add(order);
    await db.SaveChangesAsync();
    return Results.Created($"/orders/{order.Id}", order);
})
.WithName("CreateOrder")
.WithOpenApi();

group.MapPut("/{id:int}", async ([FromRoute] int id, [FromBody] UpdateOrderDto dto, AppDb db) =>
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return Results.NotFound();
    if (!string.IsNullOrWhiteSpace(dto.Customer)) order.Customer = dto.Customer!.Trim();
    if (dto.Total.HasValue) order.Total = dto.Total.Value;
    if (!string.IsNullOrWhiteSpace(dto.Status)) order.Status = dto.Status!.Trim();
    await db.SaveChangesAsync();
    return Results.Ok(order);
})
.WithName("UpdateOrder")
.WithOpenApi();

group.MapDelete("/{id:int}", async ([FromRoute] int id, AppDb db) =>
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return Results.NotFound();
    db.Orders.Remove(order);
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteOrder")
.WithOpenApi();

// Kestrel bind to 0.0.0.0:8080 for Docker
app.Urls.Add("http://0.0.0.0:8080");

app.Run();

// For WebApplicationFactory in tests
public partial class Program { }

namespace OrderService.Models
{
    public record CreateOrderDto(string Customer, decimal Total, string? Status);
    public record UpdateOrderDto(string? Customer, decimal? Total, string? Status);
}
