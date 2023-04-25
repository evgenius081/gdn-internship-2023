using System.Text;
using BankApp.API.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace BankApp.Tests
{
    /// <summary>
    /// Class containing tests for <see cref="BankService"/>.
    /// </summary>
    public class BankServiceTests
    {
        private IConfiguration configuration;
        private IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Setup for tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var appSettings = @"{""AppSettings"":{
                ""AverageRateTableName"": ""A"",
                ""BuySellRatesTableName"": ""C""
            }}";

            var builder = new ConfigurationBuilder();

            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));

            this.configuration = builder.Build();

            var mockFactory = new Mock<IHttpClientFactory>();
            var clientHandlerStub = new DelegatingHandlerStub();
            var client = new HttpClient(clientHandlerStub);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            this.httpClientFactory = mockFactory.Object;
        }

        /// <summary>
        /// Tests correct behaviour of <see cref="BankService.BankService(IHttpClientFactory, IConfiguration)"/>
        /// method.
        /// </summary>
        [Test]
        public void TestCtorCorrect()
        {
            // Assert
            Assert.DoesNotThrow(() => new BankService(this.httpClientFactory, this.configuration));
        }

        /// <summary>
        /// Tests throwing <see cref="ArgumentNullException"/> when passing null to any of arguments of
        /// <see cref="BankService.BankService(IHttpClientFactory, IConfiguration)"/> method.
        /// </summary>
        [Test]
        public void TestCtorArgumentNull()
        {
            // Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type. I know that
// it is not a good practice, but I did not have choice :(.
            Assert.Throws<ArgumentNullException>(() => new BankService(null, this.configuration));
            Assert.Throws<ArgumentNullException>(() => new BankService(this.httpClientFactory, null));
            Assert.Throws<ArgumentNullException>(() => new BankService(null, null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Tests throwing <see cref="ArgumentException"/> when passing incorrect date or currency code to
        /// <see cref="BankService.GetAverageExchangeRate(string, string)"/> method.
        /// </summary>
        /// <param name="date">Tested date values.</param>
        /// <param name="code">Tested currecny code values.</param>
        [TestCase("", "AUD")]
        [TestCase(null, "AUD")]
        [TestCase("01/01/2023", "AUD")]
        [TestCase("01-01-2023", "AUD")]
        [TestCase("2023/01/01", "AUD")]
        [TestCase("foo", "AUD")]
        [TestCase("2023-01-01", "")]
        [TestCase("2023-01-01", null)]
        [TestCase("2023-01-01", "FOO")]
        [TestCase("", "")]
        [TestCase(null, null)]
        [TestCase("01-01-2023", "FOO")]
        public void TestGetAverageExchangeRateArgumentException(string date, string code)
        {
            // Arrange
            var service = new BankService(this.httpClientFactory, this.configuration);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => service.GetAverageExchangeRate(date, code));
        }

        /// <summary>
        /// Tests throwing <see cref="ArgumentException"/> when passing incorrect date or currency code to
        /// <see cref="BankService.GetMajorDifference(string, int)"/> and
        /// <see cref="BankService.GetMaxMinExchangeRates(string, int)"/> methods.
        /// </summary>
        /// <param name="code">Tested currecny code values.</param>
        /// <param name="quotationNumber">Tested quotation number values.</param>
        [TestCase("", 1)]
        [TestCase(null, 1)]
        [TestCase("FOO", 1)]
        [TestCase("AUD", 0)]
        [TestCase("AUD", 256)]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        [TestCase("FOO", 0)]
        public void TestGetMajorDifferenceAndMinMaxArgumentException(string code, int quotationNumber)
        {
            // Arrange
            var service = new BankService(this.httpClientFactory, this.configuration);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => service.GetMajorDifference(code, quotationNumber));
            Assert.ThrowsAsync<ArgumentException>(() => service.GetMaxMinExchangeRates(code, quotationNumber));
        }
    }
}