using Microsoft.AspNetCore.Http;

namespace AspNetCore.ResponseCacheValidation
{
    public interface IResponseCacheValidator
    {
        Task<string> GetEntityTagAsync(HttpContext context);
    }
}
