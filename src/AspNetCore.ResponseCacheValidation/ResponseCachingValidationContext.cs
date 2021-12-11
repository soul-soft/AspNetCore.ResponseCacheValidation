using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.ResponseCacheValidation
{
    public class ResponseCachingValidationContext
    {
        public HttpContext HttpContext { get; }
        public string EntityHashCode { get; }
        public ResponseCachingValidationContext(HttpContext context, string entityHashCode)
        {
            HttpContext = context;
            EntityHashCode = entityHashCode;
        }
    }
}
