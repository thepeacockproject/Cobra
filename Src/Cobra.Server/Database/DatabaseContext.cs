using Cobra.Server.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions)
            : base(dbContextOptions)
        {
            //Do nothing
        }
    }
}
