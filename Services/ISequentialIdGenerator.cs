namespace urlshortenerbackend.Services;

public interface ISequentialIdGenerator
{
    Task<long> GenerateIdAsync();
}
