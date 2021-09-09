using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Shyjus.BrowserDetection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace tests
{
    class ServiceBlTest
    {
        private IServiceBl _service;
        private Mock<IProjectContext> mockContext;
        private Mock<IBrowserDetector> mockBrowserDetector;
        private Mock<DbSet<Url>> mockSet;
        private Mock<IConfiguration> mockConfig;
        private Mock<IConfigurationSection> mockConfSection;

        [SetUp]
        public void Setup()
        {
            mockSet = new Mock<DbSet<Url>>();
            mockContext = new Mock<IProjectContext>();
            mockConfig = new Mock<IConfiguration>();
            var expected = 1;

            mockBrowserDetector = new Mock<IBrowserDetector>();
            mockConfSection = new Mock<IConfigurationSection>();
            var mockConfSection2 = new Mock<IConfigurationSection>();


            var data = new List<Url>
            {
                new Url { ShortUrl = "QUMLI" },
                new Url { ShortUrl = "HMJLP" },
                new Url { ShortUrl = "GGVXA" }
            }.AsQueryable();

            var q = mockSet.As<IQueryable<Url>>();
                q.Setup(m => m.Provider).Returns(data.Provider);

            q.Setup(m => m.Provider).Returns(data.Provider);
            q.Setup(m => m.Expression).Returns(data.Expression);
            q.Setup(m => m.ElementType).Returns(data.ElementType);
            q.Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            mockContext.Setup(c => c.Urls).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(expected);            

            mockConfSection
               .Setup(x => x.Value)
               .Returns("5");

            mockConfSection2
              .Setup(x => x.Value)
              .Returns("ABCDEFGHIJKLMNOPQRSTUVWXYZ");


            mockConfig.Setup(x => x.GetSection("NewURL:amountLetters")).Returns(mockConfSection.Object);
            mockConfig.Setup(x => x.GetSection("NewURL:allowedLetters")).Returns(mockConfSection2.Object);

            _service = new ServiceBl(mockContext.Object, mockBrowserDetector.Object, mockConfig.Object);
            
        }

        [Test]
        public void Test_Add_Url_Invalid_Url_Fails()
        {
            int result = _service.AddUrl("ABCDE");
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Test_Add_Url_Valid_Url_Pass()
        {
            int result = _service.AddUrl("https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J");
            mockSet.Verify(m => m.Add(It.IsAny<Url>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Test_Get_Urls()
        {
            var model = _service.GetUrls();

            Assert.AreEqual(3, model.Urls.Count());
            Assert.AreEqual("QUMLI", model.Urls.ElementAt(0).ShortUrl);
            Assert.AreEqual("HMJLP", model.Urls.ElementAt(1).ShortUrl);
            Assert.AreEqual("GGVXA", model.Urls.ElementAt(2).ShortUrl);

        }

        [Test]
        public void Test_Unique_Url()
        {
            bool exist =_service.ExistUrl("QUMLI");
            Assert.IsTrue(exist);

        }


        [Test]
        public void Test_Create_Short_Url()
        {
            string shortUrl = _service.CreateShortUrl("https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J");            
            bool isUpperCase = shortUrl.Any(char.IsUpper);
            bool containsSpecialCharacter = shortUrl.Any(c => !char.IsLetterOrDigit(c));
            bool containsSpaces = shortUrl.Any(x => Char.IsWhiteSpace(x));

            Assert.AreEqual(shortUrl.Length, 5);
            Assert.AreEqual(isUpperCase, true);
            Assert.AreEqual(containsSpecialCharacter, false);
            Assert.AreEqual(containsSpaces, false);

        }        

        [Test]
        public void Test_Validate_Url()
        {
            bool validUrl = _service.ValidateUrl("https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J");
            Assert.IsTrue(validUrl);
        }    


    }
}
