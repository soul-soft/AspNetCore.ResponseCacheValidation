using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.ResponseCacheValidation
{
    public class ResponseCachingValidatorDescription
    {
        public string Name { get; set; }
       
        public Type ValidatorType { get; set; }

        public ResponseCachingValidatorDescription(string name, Type validatorType)
        {
            Name = name;
            ValidatorType = validatorType;
        }
    }
}
