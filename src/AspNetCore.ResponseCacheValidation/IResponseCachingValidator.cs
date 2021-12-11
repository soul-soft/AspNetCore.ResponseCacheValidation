using Microsoft.AspNetCore.Http;

namespace AspNetCore.ResponseCacheValidation
{
    public interface IResponseCachingValidator
    {
        Task<string> GetEntityHashCodeAsync(HttpContext context);
    }
}
