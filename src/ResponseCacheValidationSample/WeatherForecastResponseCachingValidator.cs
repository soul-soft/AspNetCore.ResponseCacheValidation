using AspNetCore.ResponseCacheValidation;

namespace ResponseCacheValidationSample
{
    public class WeatherForecastResponseCachingValidator 
        : IResponseCachingValidator
    {
        public Task<string> GetEntityHashCodeAsync(HttpContext context)
        {
            return Task.FromResult("0e5e1bcea4393cae");
        }
    }
}
