using System.ComponentModel.DataAnnotations;

using Claims;

namespace Claims.Interchange
{
    public record Claim
    {
      //Useful for Primitive Obsession
        //public string? Id { get; set; }

        [Required]
        public string? CoverId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public ClaimType Type { get; set; }

        [Required]
        [Range(0, 1_000_000_000, ErrorMessage = "Damage cost must be between 0 and 1,000,000,000.")]
        public decimal DamageCost { get; set; }
    }
}
