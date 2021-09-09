using hey_url_challenge_code_dotnet.ViewModels;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.Extensions.Configuration;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Models
{
    public class ServiceBl : IServiceBl
    {
        private IProjectContext _context;
        private static readonly Random getrandom = new();
        private readonly IBrowserDetector browserDetector;
        private readonly IConfiguration _configuration;

        public ServiceBl(IProjectContext context, IBrowserDetector browserDetector, IConfiguration iconfig)
        {
            _context = context;
            this.browserDetector = browserDetector;
            _configuration = iconfig;
        }

        public int AddUrl(string url)
        {
            try
            {
                if (ValidateUrl(url))
                {
                    var newUrlObj = new Url()
                    {
                        ShortUrl = CreateShortUrl(url),
                        OriginalUrl = url,
                        Count = 0,
                        Created = DateTime.Now
                    };

                    _context.Urls.Add(newUrlObj);
                    int rowsAffected = _context.SaveChanges();
                    return rowsAffected;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            
        }

        public bool ExistUrl(string newUrl)
        {
            if (_context.Urls.Any(obj => obj.ShortUrl == newUrl))
            {
                return true;
            }
            return false; 
        }

        public HomeViewModel GetUrls()
        {         
            try
            {
                var model = new HomeViewModel
                {
                    Urls = _context.Urls.ToList()
                };
                return model;
            }
            catch (Exception)
            {

                throw;
            }              
        }

        public string  CreateShortUrl(string bigUrl)
        {           
            return RandomString(_configuration.GetValue<int>("NewURL:amountLetters"));
        }

        public bool ValidateUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        public void SaveMetrics(string shortUrl)
        {
            try
            {
                Url urlObj = GetURLByShortUrl(shortUrl);

                UpdateClickCount(urlObj);

                Metric metric = new()
                {
                    OS = browserDetector.Browser.OS,
                    Broswer = browserDetector.Browser.Name,
                    URLId = urlObj.Id,
                    Clicked = DateTime.Now

                };
                _context.Metric.Add(metric);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateClickCount(Url urlObj)
        {
            urlObj.Count++;
        }

        public ShowViewModel GetMetrics(string shortUrl)
        {
            Url urlObj = _context.Urls.Where(url => url.ShortUrl == shortUrl).FirstOrDefault();

            List<Metric> metricList = _context.Metric.Where(metric => metric.URLId == urlObj.Id).ToList();

            var browseClicks = new Dictionary<string, int>();
            var platformClicks = new Dictionary<string, int>();
            var dailyClicks = new Dictionary<string, int>();

            var diffBrowsers = metricList.Select(l => l.Broswer).Distinct();
            var diffOS = metricList.Select(l => l.OS).Distinct();
            var diffDays = metricList.Select(l => l.Clicked.Day).Distinct();

            foreach (var item in diffBrowsers)
            {
                int count = metricList.Where(m => m.Broswer == item).Count();

                browseClicks.Add(item,count);
            }

            foreach (var item in diffOS)
            {
                int count = metricList.Where(m => m.OS == item).Count();

                platformClicks.Add(item, count);
            }

            foreach (var item in diffDays)
            {
                int count = metricList.Where(m => m.Clicked.Day == item).Count();

                dailyClicks.Add(item.ToString(), count);
            }


            var view = new ShowViewModel
            {
                Url = urlObj,
                DailyClicks = dailyClicks,
                BrowseClicks = browseClicks,
                PlatformClicks = platformClicks
                
            };

            return view;

        }

        public string RandomString(int length)
        {
            string chars = _configuration.GetValue<string>("NewURL:allowedLetters");
            var newString  = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[getrandom.Next(s.Length)]).ToArray());

            if (ExistUrl(newString))
            {
                RandomString(_configuration.GetValue<int>("NewURL:amountLetters")); 
            }
            return newString;
        }

        public Url GetURLByShortUrl(string shortUrl)
        {
            return _context.Urls.Where(url=>url.ShortUrl == shortUrl).FirstOrDefault();
        }
    }
}
