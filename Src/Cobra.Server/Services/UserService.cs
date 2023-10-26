using Cobra.Server.Models;
using Cobra.Server.Shared.Interfaces;

namespace Cobra.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ulong GetCurrentUserId()
        {
            return (_httpContextAccessor.HttpContext?.User.Identity as CustomIdentity)?.SteamId ?? 0UL;
        }
    }
}
