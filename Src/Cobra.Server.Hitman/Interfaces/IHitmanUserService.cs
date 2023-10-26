using Cobra.Server.Hitman.Models;

namespace Cobra.Server.Hitman.Interfaces
{
    public interface IHitmanUserService
    {
        Task<GetUserOverviewData> GetUserOverviewData(ulong userId);
        Task<int?> GetUserWallet(ulong userId);
        Task UpdateUserInfo(ulong userId, string displayName, int country, List<ulong> friends);
    }
}
