using Microsoft.EntityFrameworkCore;
using UrlShortener.Context;
using UrlShortener.Contracts;
using UrlShortener.Entities;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly Random _random = new();
        private readonly ApplicationDbContext _context;

        public UrlService(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public async Task<ShortenUrlResponse> CreateShortenedUrlAsync(ShortenUrlRequest request, HttpContext httpContext)
        {
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
            {
                return new ShortenUrlResponse
                {
                    IsSuccessful = false,
                    ErrorMessage = "The specified URL is invalid."
                };
            }

            var code = await GenerateUniqueCode();
            var shortenedUrl = new ShortenedUrl
            {
                Id = Guid.NewGuid(),
                LongUrl = request.Url,
                Code = code,
                ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/url/{code}",
                CreatedOnUtc = DateTime.UtcNow
            };

            _context.ShortenedUrls.Add(shortenedUrl);
            await _context.SaveChangesAsync();

            return new ShortenUrlResponse
            {
                IsSuccessful = true,
                ShortUrl = shortenedUrl.ShortUrl
            };
        }

        public async Task<string> RedirectUrlAsync(string code)
        {
            var shortenedUrl = await _context.ShortenedUrls
                .FirstOrDefaultAsync(s => s.Code == code);

            if (shortenedUrl == null)
            {
                return ("No URL found for the provided code.");
            }

            return(shortenedUrl.LongUrl);
        }

        public async Task<UrlResponse> GetOriginalUrlAsync(string code)
        {
            var shortenedUrl = await _context.ShortenedUrls
                .FirstOrDefaultAsync(s => s.Code == code);

            if (shortenedUrl == null)
            {
                return new UrlResponse
                {
                    IsSuccessful = false,
                    ErrorMessage = "No URL found for the provided code."
                };
            }

            return new UrlResponse
            {
                IsSuccessful = true,
                OriginalUrl = shortenedUrl.LongUrl
            };
        }

        public async Task<string> GenerateUniqueCode()
        {
           var codeChars = new char[NumberOfCharsInShortLink];
           while (true)
           {
                for (var i = 0; i < NumberOfCharsInShortLink; i++)
                {
                    var randomIndex = _random.Next(Alphabet.Length - 1);
                    codeChars[i] = Alphabet[randomIndex];
                }
                var code = new string(codeChars);

                if (!await _context.ShortenedUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
                return code;
           }
        }

        public const int NumberOfCharsInShortLink = 9;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    }
}




    

   

