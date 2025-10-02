using Cobra.Server.Edm.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cobra.Server.Edm.Mvc
{
    public class SplitNormalizedStringModelBinder : IModelBinder
    {
        private static readonly char[] _separator = new[] { ',', ';' };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //NOTE: Split by comma or semicolon
            var value = bindingContext
                .NormalizeString()?
                .Split(
                    _separator,
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                )
                .ToList();

            bindingContext.Result = ModelBindingResult.Success(value);

            return Task.CompletedTask;
        }
    }
}
