using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using urlshortenerbackend.Dto.ShortUrl;
using urlshortenerbackend.Models;
using urlshortenerbackend.Repositories;
using urlshortenerbackend.Services;
using urlshortenerbackend.Utils;

namespace urlshortenerbackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortUrlController : ControllerBase
{
    private readonly IShortUrlRepository _shortUrlRepository;
    private readonly ISequentialIdGenerator _idGenerator;
    private readonly ILogger<ShortUrlController> _logger;

    public ShortUrlController(IShortUrlRepository shortUrlRepository, ISequentialIdGenerator idGenerator ,ILogger<ShortUrlController> logger)
    {
        _shortUrlRepository = shortUrlRepository;
        _idGenerator = idGenerator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ShortUrlResponse>>> GetAllShortUrls()
    {
        var shortUrls = await _shortUrlRepository.GetAllShortUrlsAsync();
        var response = shortUrls.Select(u => new ShortUrlResponse
        {
            Alias = u.Alias,
            DestinationUrl = u.DestinationUrl
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{alias}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ShortUrlResponse>> GetShortUrlByAlias(string alias)
    {
        var shortUrl = await _shortUrlRepository.GetByAliasAsync(alias);

        if (shortUrl == null)
        {
            return NotFound($"ShortUrl with alias '{alias}' not found.");
        }

        return Ok(new ShortUrlResponse
        {
            Alias = shortUrl.Alias,
            DestinationUrl = shortUrl.DestinationUrl
        });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ShortUrlResponse>> CreateShortUrl([FromBody] CreateUpdateShortUrlRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            long newId = await _idGenerator.GenerateIdAsync();

            string generatedAlias = Base62Encoder.Encode(newId);

            var newShortUrl = new ShortUrl
            {
                Id = newId, 
                Alias = generatedAlias,
                DestinationUrl = request.DestinationUrl
            };

            var createdUrl = await _shortUrlRepository.CreateAsync(newShortUrl);

            var response = new ShortUrlResponse
            {
                Alias = createdUrl.Alias,
                DestinationUrl = createdUrl.DestinationUrl
            };

            return CreatedAtAction(nameof(GetShortUrlByAlias), new { alias = createdUrl.Alias }, response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating ShortUrl.");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut("{alias}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ShortUrlResponse>> UpdateShortUrl(string alias, [FromBody] CreateUpdateShortUrlRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingShortUrl = await _shortUrlRepository.GetByAliasAsync(alias.ToLowerInvariant());
        if (existingShortUrl == null)
        {
            return NotFound($"ShortUrl with alias '{alias}' not found.");
        }

        existingShortUrl.DestinationUrl = request.DestinationUrl;

        var updatedUrl = await _shortUrlRepository.UpdateAsync(alias.ToLowerInvariant(), existingShortUrl);

        return Ok(new ShortUrlResponse
        {
            Alias = updatedUrl.Alias,
            DestinationUrl = updatedUrl.DestinationUrl
        });
    }

    [HttpDelete("{alias}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteShortUrl(string alias)
    {
        var shortUrl = await _shortUrlRepository.GetByAliasAsync(alias.ToLowerInvariant());
        if (shortUrl == null)
        {
            return NotFound($"ShortUrl with alias '{alias}' not found.");
        }

        await _shortUrlRepository.RemoveAsync(alias.ToLowerInvariant());
        return NoContent();
    }
}
