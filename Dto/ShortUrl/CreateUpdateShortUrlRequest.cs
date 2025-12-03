using System.ComponentModel.DataAnnotations;

namespace urlshortenerbackend.Dto.ShortUrl;

public class CreateUpdateShortUrlRequest
{
    [Required(ErrorMessage = "DestinationUrl is required.")]
    [Url(ErrorMessage = "DestinationUrl must be a valid URL.")]
    public required string DestinationUrl { get; set; }
}
