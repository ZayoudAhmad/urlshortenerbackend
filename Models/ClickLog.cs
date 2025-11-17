using System;
using System.ComponentModel.DataAnnotations;
using urlshortenerbackend.Enums;

namespace urlshortenerbackend.Models;

public class ClickLog
{
    public int Id { get; set; }

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    public string? Referrer { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public DeviceType? DeviceType { get; set; }

    public Source? Source { get; set; }

    public long LinkId { get; set; }

    public required Link Link { get; set; }
}
