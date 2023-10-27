using Cobra.Server.Database;
using Cobra.Server.Database.Models;
using Cobra.Server.Hitman.Interfaces;
using Cobra.Server.Hitman.Models;
using EFCore.BulkExtensions;
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

        public async Task UpdateUserInfo(ulong userId, string displayName, int country, HashSet<ulong> friends)
        {
            await using var databaseContext = await _databaseContextFactory.CreateDbContextAsync();

            var user = await databaseContext.Users
                .Include(x => x.Friends)
                .AsSplitQuery()
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

            //Remove old friends
            foreach (var userFriend in user.Friends.Where(friend => !friends.Contains(friend.SteamId)))
            {
                databaseContext.Entry(userFriend).State = EntityState.Deleted;
            }

            //Add new friends
            var existingFriends = user.Friends
                .Select(x => x.SteamId)
                .ToHashSet();

            foreach (var friend in friends.Where(friend => !existingFriends.Contains(friend)))
            {
                user.Friends.Add(new UserFriend
                {
                    User = user,
                    SteamId = friend
                });
            }

            await databaseContext.BulkSaveChangesAsync();
        }
    }
}
