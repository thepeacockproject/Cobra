using Cobra.Server.Database;
using Cobra.Server.Database.Models;
using Cobra.Server.Hitman.Interfaces;
using Cobra.Server.Hitman.Models;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Hitman.Services
{
    public class HitmanUserService : IHitmanUserService
    {
        private readonly IDbContextFactory<DatabaseContext> _databaseContextFactory;

        public HitmanUserService(IDbContextFactory<DatabaseContext> databaseContextFactory)
        {
            _databaseContextFactory = databaseContextFactory;
        }

        public async Task<GetUserOverviewData> GetUserOverviewData(ulong userId)
        {
            await using var databaseContext = await _databaseContextFactory.CreateDbContextAsync();

            var user = await databaseContext.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            //TODO: Perform a specific (agnostic) query to satisfy as much data as possible
            return new GetUserOverviewData
            {
                WalletAmount = user.Wallet,
                ContractPlays = user.ContractPlays,
                CompetitionPlays = user.CompetitionPlays,
                TrophiesEarned = user.Trophies
            };
        }

        public async Task<int?> GetUserWallet(ulong userId)
        {
            await using var databaseContext = await _databaseContextFactory.CreateDbContextAsync();

            var user = await databaseContext.Users.FindAsync(userId);

            return user?.Wallet;
        }

        public async Task UpdateUserInfo(ulong userId, string displayName, int country, List<ulong> friends)
        {
            await using var databaseContext = await _databaseContextFactory.CreateDbContextAsync();

            var user = await databaseContext.Users
                .Include(x => x.Friends)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                user = new User
                {
                    Id = userId
                };

                databaseContext.Users.Add(user);
            }

            user.DisplayName = displayName;
            user.Country = country;

            //NOTE: This will effectively drop old friends and add new ones
            user.Friends = friends.Select(x => new UserFriend
            {
                User = user,
                SteamId = x
            }).ToList();

            await databaseContext.SaveChangesAsync();
        }
    }
}
