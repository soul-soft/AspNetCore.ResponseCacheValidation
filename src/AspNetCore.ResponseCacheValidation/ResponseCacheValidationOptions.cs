namespace AspNetCore.ResponseCacheValidation
{
    public class ResponseCacheValidationOptions
    {
        public List<ResponseCacheValidatorDescription> CacheValidatorDescriptions { get; } = new List<ResponseCacheValidatorDescription>();
       
        public void AddCacheValidator<TCacheValidator>(string name)
            where TCacheValidator : IResponseCacheValidator
        {
            CacheValidatorDescriptions.Add(new ResponseCacheValidatorDescription(name, typeof(TCacheValidator)));
        }
    }
}
