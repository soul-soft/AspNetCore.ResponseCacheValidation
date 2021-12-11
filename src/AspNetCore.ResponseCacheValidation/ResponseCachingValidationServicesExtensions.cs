using AspNetCore.ResponseCacheValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponseCachingValidationServicesExtensions
    {
        public static IServiceCollection AddResponseCacheValidation(this IServiceCollection services, Action<ResponseCachingValidationOptions> configure)
        {
            services.Configure(configure);
            services.AddTransient<IResponseCachingValidationProvider, ResponseCachingValidationProvider>();
            return services;
        }
    }
}
