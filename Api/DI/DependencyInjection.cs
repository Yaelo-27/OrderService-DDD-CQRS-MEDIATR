namespace Api.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddOpenApi();

            return services;
        }
    }
}