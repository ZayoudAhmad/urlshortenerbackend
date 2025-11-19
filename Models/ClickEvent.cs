namespace urlshortenerbackend.Models;

public class ClickEvent
{
    public long Id { get; set; }

    public string Alias { get; set; } = default!;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string? Country { get; set; }

    public string? Device { get; set; }

    public string? Os { get; set; }

    public string? Referrer { get; set; }

    public string IpAddress { get; set; } = default!;
}
