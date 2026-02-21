using Api.DI;
using Api.Extensions;
using Aplication.DI;
using Infrastructure.DI;
using MediatR;
using Aplication.Orders;
using Aplication.dto;

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
app.UseRateLimiter();

app.UseHttpsRedirection();

// Orders endpoints
app.MapPost("/orders", async (CreateOrderCommand command, IMediator mediator, HttpResponse response) =>
{
    var orderId = await mediator.Send(command);
    if(orderId is null) return Results.BadRequest($"The order hasn't been placed, an error ocurred");

    return Results.Created<Guid>($"/orders/{orderId.Value}",orderId.Value);
})
.WithName("CreateOrder");

app.MapGet("/orders/{id}", async (IMediator mediator, string id, HttpResponse response) =>
{
    if(string.IsNullOrEmpty(id)) return Results.NotFound("there is not order id given");
    var order = await mediator.Send(new GetOrderQuery(new Guid(id)));
    if (order is null) return Results.NotFound("There are not orders place with the id given");

    return Results.Ok<OrderDto>(order);
})
.WithName("GetOrder");

app.Run();

// Expose Program for integration testing
public partial class Program { }