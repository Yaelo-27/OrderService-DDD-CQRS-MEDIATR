using Aplication.data;
using Azure.Core.Pipeline;
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

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
            });
            //Scrutor library
            services.Scan(scan => scan
                                    .FromAssemblyOf<InfrastructureAssemblyReference>()
                                    .AddClasses(classes => classes
                                                        .Where(type => type.Name.EndsWith("Service")))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());

            return services;
        }
    }
}