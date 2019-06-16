using Microsoft.AspNetCore.Mvc;
using System;
using UrlShortener.Helpers;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class ShortUrlsController : Controller
    {
        private readonly IShortUrlService _service;
        private readonly System.Random idGen;

        public ShortUrlsController(IShortUrlService service)
        {
            _service = service;
            idGen = new System.Random();
        }

        public IActionResult Index()
        {
            return RedirectToAction(actionName: nameof(Create));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string originalUrl)
        {
            var shortUrl = new ShortUrl
            {
                UrlId = idGen.Next(),
                OriginalUrl = originalUrl,
                CreatedDateTime = DateTime.Now
            };

            TryValidateModel(shortUrl);
            if (ModelState.IsValid)
            {
                _service.SaveAsync(shortUrl);

                return RedirectToAction(actionName: nameof(Show), routeValues: new { id = shortUrl.UrlId });
            }

            return View(shortUrl);
        }

        public IActionResult Show(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var shortUrl = _service.GetByIdAsync(id.Value);
            if (shortUrl == null) 
            {
                return NotFound();
            }

            var result = shortUrl.Result;

            ViewData["Path"] = ShortUrlHelper.Encode(result.UrlId);

            return View(result);
        }

        [HttpGet("/ShortUrls/RedirectTo/{path:required}", Name = "ShortUrls_RedirectTo")]
        public IActionResult RedirectTo(string path)
        {
            if (path == null) 
            {
                return NotFound();
            }

            var shortUrl = _service.GetByPathAsync(path);
            if (shortUrl == null) 
            {
                return NotFound();
            }

            return Redirect(shortUrl.Result.OriginalUrl);
        }
    }
}
