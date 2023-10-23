using Cobra.Server.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Attributes
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
