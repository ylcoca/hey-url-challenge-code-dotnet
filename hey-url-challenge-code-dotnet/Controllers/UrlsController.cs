using System;
using hey_url_challenge_code_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller 
    {
        private readonly ILogger<UrlsController> _logger;
        public IServiceBl _service;

        public UrlsController(ILogger<UrlsController> logger, IServiceBl service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {            
            return View(_service.GetUrls());
        }

        [Route("/{url}")]
        public IActionResult Visit(string url)
        {
            try
            {
                _service.SaveMetrics(url);
                return Redirect(_service.GetURLByShortUrl(url).OriginalUrl);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(PageNotFound));
            }
                       
        }

        [Route("urls/{url}")]
        public IActionResult Show(string url) {
            try
            {
                return View(_service.GetMetrics(url));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(PageNotFound));
            }
                  
        }

        [Route("urls/Create")]
        [HttpPost]
        public IActionResult Create(string url)
        {
            try
            {
                int result = _service.AddUrl(url);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(PageNotFound));
            }
            
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {            
            return View();
        }
    }
}