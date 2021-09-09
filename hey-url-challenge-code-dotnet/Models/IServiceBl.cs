using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Models
{
    public interface IServiceBl
    {
        public int AddUrl(string url);
        public string CreateShortUrl(string bigUrl);
        public bool ValidateUrl(string url);
        public void SaveMetrics(string url);
        public ShowViewModel GetMetrics(string url);       
        public HomeViewModel GetUrls();
        public Url GetURLByShortUrl(string shortUrl);
        bool ExistUrl(string newUrl);




    }
}
