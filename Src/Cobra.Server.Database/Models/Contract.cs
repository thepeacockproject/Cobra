using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cobra.Server.Database.Enums;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database.Models
{
    [Index(nameof(DisplayId), IsUnique = true)]
    public class Contract
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public uint Id { get; set; }

        [Required]
        public string DisplayId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public User User { get; set; }

        public int LevelIndex { get; set; }
        public int CheckpointIndex { get; set; }
        public int Difficulty { get; set; }
        public int ExitId { get; set; }
        public int WeaponToken { get; set; }
        public int OutfitToken { get; set; }

        [Required]
        public ContractTarget Target1 { get; set; }

        public ContractTarget Target2 { get; set; }
        public ContractTarget Target3 { get; set; }
        public EContractRestrictionType Restrictions { get; set; }
    }
}
