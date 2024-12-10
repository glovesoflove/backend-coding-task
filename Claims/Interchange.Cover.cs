using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Claims.Interchange;

public class Cover
{
  [Required]
    public string? Id { get; set; }
  
  [Required]
    public DateTime StartDate { get; set; }

[Required]
    public DateTime EndDate { get; set; }

[Required]
    public CoverType Type { get; set; }

[Required]
    public decimal Premium { get; set; }
}
