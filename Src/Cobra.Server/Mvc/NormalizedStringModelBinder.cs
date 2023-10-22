using Cobra.Server.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cobra.Server.Mvc
{
    public class NormalizedStringModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.NormalizeString();

            bindingContext.Result = ModelBindingResult.Success(value);

            return Task.CompletedTask;
        }
    }
}