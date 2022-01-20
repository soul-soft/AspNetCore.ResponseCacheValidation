using Microsoft.AspNetCore.Http;

namespace AspNetCore.ResponseCacheValidation
{
    public interface IResponseCacheValidationProvider
    {
        bool AllowResponseCacheValidation(HttpContext context);
        IResponseCacheValidator GetResponseCacheValidator(HttpContext context);
    }
}
