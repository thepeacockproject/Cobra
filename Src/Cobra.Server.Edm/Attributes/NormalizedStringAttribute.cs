using Cobra.Server.Edm.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Edm.Attributes
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
