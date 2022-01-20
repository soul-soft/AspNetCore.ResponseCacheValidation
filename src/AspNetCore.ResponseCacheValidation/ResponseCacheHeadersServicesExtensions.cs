using AspNetCore.ResponseCacheValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponseCacheHeadersServicesExtensions
    {
        public static IServiceCollection AddResponseCacheValidation(this IServiceCollection services, Action<ResponseCacheValidationOptions> options)
        {
            services.Configure(options);
            services.TryAddTransient<IResponseCacheValidationProvider, ResponseCacheValidationProvider>();
            return services;
        }
    }
}
