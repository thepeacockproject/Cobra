using Cobra.Server.Edm.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Edm.Attributes
{
    public class NormalizedJsonStringAttribute : ModelBinderAttribute
    {
        public NormalizedJsonStringAttribute(string propertyName = null)
            : base(typeof(NormalizedJsonStringModelBinder))
        {
            Name = propertyName;
        }
    }
}
