using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.Database.Migrate();
        }
    }
}