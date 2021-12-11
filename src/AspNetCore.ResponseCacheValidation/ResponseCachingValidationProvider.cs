using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AspNetCore.ResponseCacheValidation
{
    internal class ResponseCachingValidationProvider 
        : IResponseCachingValidationProvider
    {
        private readonly ResponseCachingValidationOptions _options;
      
        public ResponseCachingValidationProvider(IOptions<ResponseCachingValidationOptions> options)
        {
            _options = options.Value;
        }
      
        public IResponseCachingValidator? FindValidator(HttpContext context, string name)
        {
            var validator = _options.Validators.Where(a => a.Name == name).FirstOrDefault();
            if (validator == null)
                return null;
            return ActivatorUtilities.CreateInstance(context.RequestServices,validator.ValidatorType) as IResponseCachingValidator;
        }
    }
}
