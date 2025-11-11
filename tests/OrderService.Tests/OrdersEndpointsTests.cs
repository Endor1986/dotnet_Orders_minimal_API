using System.Net;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Xunit;

namespace OrderService.Tests;

public class OrdersEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public OrdersEndpointsTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Health_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/health");
        res.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create_And_List_Order_Works()
    {
        var client = _factory.CreateClient();

        var createPayload = new { customer = "Test", total = 12.34m, status = "new" };
        var post = await client.PostAsJsonAsync("/orders", createPayload);
        post.StatusCode.Should().Be(HttpStatusCode.Created);

        var list = await client.GetFromJsonAsync<List<OrderDto>>("/orders");
        list.Should().NotBeNull();
        list!.Any(x => x.Customer == "Test").Should().BeTrue();
    }

    // Typisiertes DTO passend zur API
    public class OrderDto
    {
        public int Id { get; set; }
        public string Customer { get; set; } = "";
        public decimal Total { get; set; }
        public string Status { get; set; } = "";
        public System.DateTimeOffset CreatedAt { get; set; }
        public long CreatedAtUnixMs { get; set; }
    }
}
