
using AspNetCore.ResponseCacheValidation;

namespace ResponseCacheValidationSample
{
    public class WeatherForecastResponseCacheValidator 
        : IResponseCacheValidator
    {
        public Task<string> GetEntityTagAsync(HttpContext context)
        {
            return Task.FromResult("0e5e1bcea4393cae");
        }
    }
}
