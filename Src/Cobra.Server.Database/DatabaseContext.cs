using Cobra.Server.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<UserContract> UserContracts { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractTarget> ContractTargets { get; set; }
        public DbSet<ScoreStory> ScoresStory { get; set; }
        public DbSet<ScoreSniper> ScoresSniper { get; set; }
        public DbSet<ScoreTutorial> ScoresTutorial { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions)
            : base(dbContextOptions)
        {
            //Do nothing
        }
    }
}
