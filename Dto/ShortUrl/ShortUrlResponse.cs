using System;

namespace urlshortenerbackend.Dto.ShortUrl;

public class ShortUrlResponse
{
    public required string Alias { get; set; }
    public required string DestinationUrl { get; set; }
}