using FluentValidation;
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
            
            // Register FluentValidation validatos from the current assembly.
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
            return services;
        }
    }
}