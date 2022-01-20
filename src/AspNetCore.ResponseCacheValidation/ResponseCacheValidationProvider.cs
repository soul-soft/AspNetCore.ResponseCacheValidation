using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AspNetCore.ResponseCacheValidation
{
    internal class ResponseCacheValidationProvider
        : IResponseCacheValidationProvider
    {
        private readonly ResponseCacheValidationOptions _options;

        public ResponseCacheValidationProvider(IOptions<ResponseCacheValidationOptions> options)
        {
            _options = options.Value;
        }
        /// <summary>
        /// 是否允许缓存验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool AllowResponseCacheValidation(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
                return false;

            var attribute = endpoint.Metadata.GetMetadata<ResponseCacheValidationAttribute>();
            if (attribute == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取缓存验证器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IResponseCacheValidator GetResponseCacheValidator(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            IResponseCacheValidator? validator = null;
            var attribute = endpoint?.Metadata.GetMetadata<ResponseCacheValidationAttribute>();
            if (attribute != null)
            {
                validator = CreateResponseCacheValidatorInstance(context, attribute.Name);
            }
            if (validator == null)
            {
                throw new InvalidOperationException($"{attribute?.Name} policy not found");
            }
            return validator;
        }
        /// <summary>
        /// 创建缓存验证器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private IResponseCacheValidator? CreateResponseCacheValidatorInstance(HttpContext context, string name)
        {
            var validator = _options.CacheValidatorDescriptions
                .Where(a => a.Name == name)
                .FirstOrDefault();
            if (validator == null)
                return null;
            return ActivatorUtilities.CreateInstance(context.RequestServices, validator.ValidatorType) as IResponseCacheValidator;
        }
    }
}
