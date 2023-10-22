using Cobra.Server.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Attributes
{
    public class SplitNormalizedStringAttribute : ModelBinderAttribute
    {
        public SplitNormalizedStringAttribute(string propertyName = null)
            : base(typeof(SplitNormalizedStringModelBinder))
        {
            Name = propertyName;
        }
    }
}