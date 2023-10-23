using Cobra.Server.Edm.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Edm.Attributes
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
