using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.ResponseCacheValidation
{
    public class ResponseCachingValidationOptions
    {
        public List<ResponseCachingValidatorDescription> Validators = new List<ResponseCachingValidatorDescription>();
       
        public void AddValidator<TValidator>(string name)
            where TValidator : IResponseCachingValidator
        {
            Validators.Add(new ResponseCachingValidatorDescription(name, typeof(TValidator)));
        }
    }
}
