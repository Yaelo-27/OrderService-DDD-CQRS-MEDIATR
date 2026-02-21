using System.Net;
using System.Threading.RateLimiting;

namespace Api.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddOpenApi();
            services.AddRateLimiter(opt =>
            {
               opt.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
               {
                   string inboundIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknonw";
                   return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: inboundIp,
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromSeconds(60),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2
                    }
                   );
               });
               opt.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
            });
            return services;
        }
    }
}