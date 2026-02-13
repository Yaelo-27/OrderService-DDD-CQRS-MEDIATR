using Microsoft.Extensions.DependencyInjection;

namespace Aplication.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR handlers from the current assembly.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly));
            
            return services;
        }
    }
}