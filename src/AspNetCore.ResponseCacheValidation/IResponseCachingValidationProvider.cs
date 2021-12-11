using Microsoft.AspNetCore.Http;

namespace AspNetCore.ResponseCacheValidation
{
    public interface IResponseCachingValidationProvider
    {
        IResponseCachingValidator? FindValidator(HttpContext context,string name);
    }
}
