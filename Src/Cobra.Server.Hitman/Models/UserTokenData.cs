using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;

namespace Cobra.Server.Hitman.Models
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
