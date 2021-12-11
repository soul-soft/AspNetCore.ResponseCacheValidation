using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.ResponseCacheValidation
{
    public class ResponseCacheValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IResponseCachingValidationProvider _provider;
        public ResponseCacheValidationMiddleware(
            RequestDelegate next,
            IResponseCachingValidationProvider provider)
        {
            _next = next;
            _provider = provider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //是否允许缓存查找
            if (!AllowCacheLookup(context))
            {
                await _next(context);
                return;
            }
            var validator = GetCachingValidator(context);
            var ifNoneMatch = context.Request.Headers.IfNoneMatch;
            var hashcode = await validator.GetEntityHashCodeAsync(context);
            if (ifNoneMatch == hashcode)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                return;
            }
            else
            {
                context.Response.Headers.ETag = new StringValues(hashcode);
                await _next(context);
                return;
            }
        }


        private static bool AllowCacheLookup(HttpContext context)
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

        private IResponseCachingValidator GetCachingValidator(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            IResponseCachingValidator? validator = null;
            var attribute = endpoint?.Metadata.GetMetadata<ResponseCacheValidationAttribute>();
            if (attribute != null)
            {
                validator = _provider.FindValidator(context, attribute.Name);
            }
            if (validator == null)
            {
                throw new InvalidOperationException($"{attribute?.Name} policy not found");
            }
            return validator;
        }
    }
}
