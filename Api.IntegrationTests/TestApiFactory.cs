using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using System;

namespace Api.IntegrationTests;

public class TestApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            if (!db.Database.CanConnect())
            {
                throw new InvalidOperationException("Database is not available. Ensure the configured database and schema exist before running tests.");
            }
        });
    }
}
