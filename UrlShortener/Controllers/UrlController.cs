using Microsoft.AspNetCore.Mvc;
using UrlShortener.Context;
using UrlShortener.Contracts;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;
        private readonly ApplicationDbContext _context;

        public UrlController(IUrlService urlService, ApplicationDbContext context)
        {
            _urlService = urlService;
            _context = context;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] ShortenUrlRequest request)
        {
            var response = await _urlService.CreateShortenedUrlAsync(request, HttpContext);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> RedirectToOriginal(string code)
        {
            var url = await _urlService.RedirectUrlAsync(code);

            if (url == null)
            {
                return NotFound();
            }

            return Redirect(url);
        }


        [HttpGet("resolve/{code}")]
        public async Task<IActionResult> GetOriginaUrlOnly(string code)
        {
            var url = await _urlService.GetOriginalUrlAsync(code);

            if (url == null)
                return NotFound();

            return Ok(new { OriginalUrl = url });
        }

    }
}

