using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cobra.Server.Database.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }

        public ulong SteamId { get; set; }
        public string DisplayName { get; set; }
        public int Country { get; set; }
        public int Wallet { get; set; }
        public int ContractPlays { get; set; }
        public int CompetitionPlays { get; set; }
        public int Trophies { get; set; }
    }
}