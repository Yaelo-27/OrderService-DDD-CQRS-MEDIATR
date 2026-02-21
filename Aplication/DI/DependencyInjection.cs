using Aplication.Common.Mapper;
using Aplication.Orders.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aplication.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR handlers from the current assembly.
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            // Register FluentValidation validatos from the current assembly.
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<OrderMappingProfile>();
            });
            return services;
        }
    }
}