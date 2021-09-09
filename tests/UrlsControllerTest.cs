using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace tests
{
    public class UrlsControllerTest
    {
        private UrlsController urlsController;
        Mock<IServiceBl> _serviceMoq;
        [SetUp]
        public void Setup()
        {
            var _loggerMoq = new Mock<ILogger<UrlsController>>();
            _serviceMoq = new Mock<IServiceBl>();           

            urlsController = new UrlsController(_loggerMoq.Object, _serviceMoq.Object);
        }

        [Test]
        public void Test_Url_Index_Exist()
        {
            var result = urlsController.Index() as ViewResult;

            Assert.IsNotNull(result);

        }
        [Test]
        public void Test_Url_Visit_Exception_To_Not_Found()
        {
            var result = urlsController.Visit("url");

            Assert.AreEqual("PageNotFound", ((RedirectToActionResult)result).ActionName);

        }

        [Test]
        public void Test_Url_Visit_Redirects_To_URL()
        {
            Url testUrl = new Url
            {
                Count = 1,
                OriginalUrl = "https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J",
                ShortUrl = "ABCD"
            };
            _serviceMoq.Setup(m => m.GetURLByShortUrl(It.IsAny<string>())).Returns(testUrl);
            var result = urlsController.Visit("url");

            Assert.AreEqual("https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J", ((RedirectResult)result).Url);

        }

        [Test]
        public void Test_Url_Show_Exist()
        {
            var result = urlsController.Show("url") as ViewResult;

            Assert.IsNotNull(result);

        }
        [Test]
        public void Test_Url_Create_Refresh_Index()
        {
            var result = urlsController.Create("url");
            Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        }

        [Test]
        public void Test_Url_Page_Not_Found_Exist()
        {
            var result = urlsController.PageNotFound() as ViewResult;

            Assert.IsNotNull(result);

        }
    }
}