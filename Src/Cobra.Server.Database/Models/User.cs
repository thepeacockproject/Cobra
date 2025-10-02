using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database.Models
{
    [Index(nameof(Country))]
    public class User
    {
        /// <remarks>This should be treated as a Steam ID.</remarks>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string DisplayName { get; set; }

        public int Country { get; set; }
        public int Wallet { get; set; }
        public int ContractPlays { get; set; }
        public int CompetitionPlays { get; set; }
        public int Trophies { get; set; }

        public ICollection<UserFriend> Friends { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
