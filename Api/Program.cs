using Api.DI;
using Api.Extensions;
using Aplication.DI;
using Infrastructure.DI;
using MediatR;
using Aplication.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
       .AddApi()
       .AddInfrastructure(builder.Configuration)
       .AddApplication(); //Add DI form all layers

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MigrateDatabase(); // Apply pending migrations at development startup
}

app.UseHttpsRedirection();

// Orders endpoints
app.MapPost("/orders", async (CreateOrderCommand command, IMediator mediator, HttpResponse response) =>
{
    var orderId = await mediator.Send(command);
    response.StatusCode = StatusCodes.Status201Created;
    response.Headers.Location = $"/orders/{orderId.Value}";
    await response.WriteAsync(orderId.Value.ToString());
    return Results.Empty;
})
.WithName("CreateOrder");

app.MapGet("/orders/{id:guid}", async (IMediator mediator, Guid id, HttpResponse response) =>
{
    var order = await mediator.Send(new GetOrderQuery(id));
    if (order is null) return Results.NotFound();
    var json = System.Text.Json.JsonSerializer.Serialize(order);
    response.ContentType = "application/json";
    await response.WriteAsync(json);
    return Results.Empty;
})
.WithName("GetOrder");

app.Run();

// Expose Program for integration testing
public partial class Program { }