using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace urlshortenerbackend.Models;

public class ClickEvent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(6)]
    public required string Alias { get; set; }

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string? Country { get; set; }

    public string? Device { get; set; }

    public string? Os { get; set; }

    public string? Referrer { get; set; }

    public string IpAddress { get; set; } = default!;
}
