using System.ComponentModel.DataAnnotations;

namespace urlshortenerbackend.Models;

public class Link
{
    public long Id { get; set; }

    [Required]
    public required string ShortUrl { get; set; }

    [Required]
    public required string DestinationUrl { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public long? FolderId { get; set; }

    public Folder? Folder { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<ClickLog> ClickLogs { get; set; } = new List<ClickLog>();
}
