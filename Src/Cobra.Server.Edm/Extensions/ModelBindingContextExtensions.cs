using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cobra.Server.Edm.Extensions
{
    public static class ModelBindingContextExtensions
    {
        public static string NormalizeString(this ModelBindingContext bindingContext)
        {
            return bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName).Values
                .FirstOrDefault()?
                .Trim('\'');
        }
    }
}
