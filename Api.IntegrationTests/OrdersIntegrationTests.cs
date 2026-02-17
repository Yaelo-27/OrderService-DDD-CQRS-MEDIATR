using System;
using System.Net.Http.Json;
using System.Net;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

namespace Api.IntegrationTests;

public class OrdersIntegrationTests : IClassFixture<TestApiFactory>
{
    private readonly TestApiFactory _factory;

    public OrdersIntegrationTests(TestApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_CreateOrder_Then_GetOrder_Returns_Created_And_Found()
    {
        var client = _factory.CreateClient();

        var createRequest = new
        {
            ShippingContactName = "Test User",
            ShippingContactEmail = "test@example.com",
            ShippingContactPhoneNumber = "+34123456789",
            ShippingAddressStreet = "123 Test St",
            ShippingAddressCity = "Testville",
            ShippingAddressState = "TS",
            ShippingAddressPostalCode = "12345",
            ShippingAddressCountry = "Testland",
            Items = new[]
            {
                new { ProductId = Random.Shared.Next(1, 100000), ProductName = "Test Product", Quantity = 1, UnitPrice = 9.99m }
            }
        };

        var postResponse = await client.PostAsJsonAsync("/orders", createRequest);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdText = await postResponse.Content.ReadAsStringAsync();
        var createdId = Guid.Parse(createdText);
        createdId.Should().NotBe(Guid.Empty);

        var getResponse = await client.GetAsync($"/orders/{createdId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var order = await getResponse.Content.ReadFromJsonAsync<object>();
        order.Should().NotBeNull();
    }
}
