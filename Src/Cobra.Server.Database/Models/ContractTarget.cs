using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server.Database.Models
{
    [Index(
        nameof(Name),
        nameof(WeaponToken), nameof(OutfitToken), nameof(AmmoType), nameof(SpecialSituation),
        IsUnique = true
    )]
    public class ContractTarget
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int WeaponToken { get; set; }
        public int OutfitToken { get; set; }
        public int AmmoType { get; set; }
        public int SpecialSituation { get; set; }
    }
}
