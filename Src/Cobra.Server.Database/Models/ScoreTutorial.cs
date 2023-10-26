using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cobra.Server.Database.Models
{
    public class ScoreTutorial
    {
        [Key, ForeignKey(nameof(User))]
        public ulong UserId { get; set; }

        public int Score { get; set; }

        public User User { get; set; }
    }
}
