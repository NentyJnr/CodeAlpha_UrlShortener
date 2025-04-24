using UrlShortener.Models;

namespace UrlShortener.Contracts
{
    public interface IUrlService
    {
        Task<ShortenUrlResponse> CreateShortenedUrlAsync(ShortenUrlRequest request, HttpContext httpContext);
        Task<string> GenerateUniqueCode();
        Task<string?> RedirectUrlAsync(string code);
        Task<UrlResponse> GetOriginalUrlAsync(string code);

    }
}

