namespace AspNetCore.ResponseCacheValidation
{
    public class ResponseCacheValidatorDescription
    {
        public string Name { get; set; }
       
        public Type ValidatorType { get; set; }

        public ResponseCacheValidatorDescription(string name, Type validatorType)
        {
            Name = name;
            ValidatorType = validatorType;
        }
    }
}
