using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using urlshortenerbackend.Enums;

namespace urlshortenerbackend.Models;

public class ClickLog
{
    [Key]
    public long Id { get; set; }

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    public string? Referrer { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public DeviceType? DeviceType { get; set; }

    public Source? Source { get; set; }

    public long LinkId { get; set; }

    [ForeignKey("LinkId")]
    public required Link Link { get; set; }
}
