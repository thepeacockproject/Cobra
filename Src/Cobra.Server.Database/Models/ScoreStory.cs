using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database.Models
{
    [PrimaryKey(nameof(Id), nameof(UserId))]
    public class ScoreStory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public uint Id { get; set; } //NOTE: Leaderboard ID

        [ForeignKey(nameof(User))]
        public ulong UserId { get; set; }

        public int Score { get; set; }

        public User User { get; set; }
    }
}
