using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;

namespace Cobra.Server.Models
{
    [EdmEntity("UserTokenData")]
    public class UserTokenData : IEdmEntity
    {
        [EdmProperty("TokenId", EdmTypes.Int32, false)]
        public int TokenId { get; set; }

        [EdmProperty("SubId", EdmTypes.Int32, false)]
        public int SubId { get; set; }

        [EdmProperty("Level", EdmTypes.Int32, false)]
        public int Level { get; set; }
    }
}
