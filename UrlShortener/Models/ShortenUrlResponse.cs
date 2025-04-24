namespace UrlShortener.Models
{
    public class ShortenUrlResponse
    {
        public bool IsSuccessful { get; set; }
        public string? ShortUrl { get; set; }
        public string? ErrorMessage { get; set; }
    }
}


