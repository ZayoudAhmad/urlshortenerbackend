using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace urlshortenerbackend.Models;

public class ShortUrlMeta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(6)]
    public required string Alias { get; set; }

    [Required]
    public required string DestinationUrl { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    [Required]    
    public long UserId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long TotalClicks { get; set; } = 0;

    public long? FolderId { get; set; }

    [ForeignKey("FolderId")]
    public Folder? Folder { get; set; }
}