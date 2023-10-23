using Cobra.Server.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Attributes
{
    public class NormalizedStringAttribute : ModelBinderAttribute
    {
        public NormalizedStringAttribute(string propertyName = null)
            : base(typeof(NormalizedStringModelBinder))
        {
            Name = propertyName;
        }
    }
}
