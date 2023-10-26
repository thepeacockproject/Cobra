using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database.Models
{
    [PrimaryKey(nameof(UserId), nameof(ContractId))]
    [Index(nameof(UserId))]
    [Index(nameof(UserId), nameof(Queued))]
    public class UserContract
    {
        [ForeignKey(nameof(User))]
        public ulong UserId { get; set; }

        [ForeignKey(nameof(Contract))]
        public uint ContractId { get; set; }

        public bool Queued { get; set; }
        public int? Plays { get; set; }
        public int? Score { get; set; }
        public bool? Liked { get; set; }
        public DateTime? LastPlayedAt { get; set; }

        public User User { get; set; }
        public Contract Contract { get; set; }
    }
}
