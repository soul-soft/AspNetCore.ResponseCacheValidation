namespace Microsoft.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ResponseCacheValidationAttribute : Attribute
    {
        public string Name { get;  }

        public ResponseCacheValidationAttribute(string name)
        {
            Name = name;
        }
    }
}
