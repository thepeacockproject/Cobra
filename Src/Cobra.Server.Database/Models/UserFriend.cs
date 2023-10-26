using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database.Models
{
    [PrimaryKey(nameof(UserId), nameof(SteamId))]
    public class UserFriend
    {
        [ForeignKey(nameof(User))]
        public ulong UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong SteamId { get; set; }

        public User User { get; set; }
    }
}
