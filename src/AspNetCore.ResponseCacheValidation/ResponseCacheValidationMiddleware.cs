using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace AspNetCore.ResponseCacheValidation
{
    public class ResponseCacheValidationMiddleware
    {
        private readonly RequestDelegate _next;
        
        private readonly IResponseCacheValidationProvider _provider;

        public ResponseCacheValidationMiddleware(
            RequestDelegate next,
            IResponseCacheValidationProvider provider)
        {
            _next = next;
            _provider = provider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //是否允许缓存查找
            if (!_provider.AllowResponseCacheValidation(context))
            {
                await _next(context);
                return;
            }
            var validator = _provider.GetResponseCacheValidator(context);
            var ifNoneMatch = context.Request.Headers.IfNoneMatch;
            var entityTag = await validator.GetEntityTagAsync(context);
            if (ifNoneMatch == entityTag)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                return;
            }
            else
            {
                context.Response.Headers.ETag = new StringValues(entityTag);
                await _next(context);
                return;
            }
        }
    }
}
