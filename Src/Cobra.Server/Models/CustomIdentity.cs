using System.Security.Claims;

namespace Cobra.Server.Models
{
    public class CustomIdentity : ClaimsIdentity
    {
        public override bool IsAuthenticated => true;

        public ulong SteamId { get; }

        public CustomIdentity(ulong steamId)
        {
            SteamId = steamId;
        }
    }
}
