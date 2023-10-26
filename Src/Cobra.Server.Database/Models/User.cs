using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database.Models
{
    [Index(nameof(Country))]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong Id { get; set; } //NOTE: SteamId

        [Required]
        public string DisplayName { get; set; }

        public int Country { get; set; }
        public int Wallet { get; set; }
        public int ContractPlays { get; set; }
        public int CompetitionPlays { get; set; }
        public int Trophies { get; set; }

        public ICollection<UserFriend> Friends { get; set; }
    }
}
