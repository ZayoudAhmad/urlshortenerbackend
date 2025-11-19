using System.ComponentModel.DataAnnotations;

namespace urlshortenerbackend.Models;

public class ShortUrlMeta
{
    public long Id { get; set; }

    public string Alias { get; set; } = default!;

    public string DestinationUrl { get; set; } = default!;

    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public long UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long TotalClicks { get; set; }

    public long? FolderId { get; set; }

    public Folder? Folder { get; set; }
}