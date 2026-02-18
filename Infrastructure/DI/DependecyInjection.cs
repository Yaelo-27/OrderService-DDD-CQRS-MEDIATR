using Aplication.data;
using Domain.Orders;
using Domain.Primitives.Domain.Interaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderDbContext>(provider => provider.GetRequiredService<OrderDbContext>());

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<OrderDbContext>());

            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}