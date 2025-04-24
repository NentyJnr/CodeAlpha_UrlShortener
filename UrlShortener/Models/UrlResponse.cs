namespace UrlShortener.Models
{
    public class UrlResponse
    {
        public bool IsSuccessful { get; set; }
        public string? OriginalUrl { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

