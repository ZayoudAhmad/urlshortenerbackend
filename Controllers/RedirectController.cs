using Microsoft.AspNetCore.Mvc;
using urlshortenerbackend.Repositories;

namespace urlshortenerbackend.Controllers;

[ApiController]
public class RedirectController : ControllerBase
{
    private readonly IShortUrlRepository _shortUrlRepository;
    private readonly ILogger<RedirectController> _logger;

    public RedirectController(IShortUrlRepository shortUrlRepository, ILogger<RedirectController> logger)
    {
        _shortUrlRepository = shortUrlRepository;
        _logger = logger;
    }

    [HttpGet("/{alias}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RedirectToDestination(string alias)
    {
        if (string.IsNullOrEmpty(alias))
        {
            _logger.LogWarning("Received request with empty alias.");
            return NotFound();
        }

        try
        {
            var shortUrl = await _shortUrlRepository.GetByAliasAsync(alias);

            if (shortUrl == null)
            {
                _logger.LogInformation("Alias '{Alias}' not found in MongoDB.", alias);
                return NotFound();
            }

            _logger.LogInformation("Redirecting alias '{Alias}' to '{DestinationUrl}'", alias, shortUrl.DestinationUrl);
            return Redirect(shortUrl.DestinationUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during redirection lookup for alias: {Alias}", alias);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error processing the redirect request.");
        }
    }
}
