using AspNetCore.ResponseCacheValidation;

namespace Microsoft.AspNetCore.Builder
{
    public static class ResponseCacheHeadersApplicationExtensions
    {
        public static IApplicationBuilder UseResponseCacheValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseCacheValidationMiddleware>();
            return app;
        }
    }
}
