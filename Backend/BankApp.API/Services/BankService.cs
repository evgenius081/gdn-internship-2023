using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.RateLimiting;
using System.Web.Http;
using BankApp.API.Interfaces;
using BankApp.API.Models.RateLists;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankApp.API.Services
{
    /// <summary>
    /// Service for dealing with NBP API.
    /// </summary>
    public class BankService : IBankService
    {
        private readonly string averageRateTableName;
        private readonly string buySellRatesTableName;
        private readonly IHttpClientFactory clientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankService"/> class.
        /// </summary>
        /// <param name="clientFactory">Factory for creating <see cref="HttpClient"/>.</param>
        /// <param name="configuration">Configuration object for extracting data from appsettings.json.</param>
        /// <exception cref="ArgumentNullException">Thrown if client factory or configuration is
        /// null.</exception>
        public BankService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.averageRateTableName = configuration.GetValue<string>("AverageRateTableName") !;
            this.buySellRatesTableName = configuration.GetValue<string>("BuySellRatesTableName") !;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown if date or currency code is not valid.</exception>
        /// <exception cref="InvalidOperationException">Thrown if there are any problems with request/response
        /// to NBP API.</exception>
        public async Task<AverageRateList> GetAverageExchangeRate(string date, string code)
        {
            if (string.IsNullOrEmpty(date) || !DateTime.TryParseExact(
                date,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime parsedDate))
            {
                throw new ArgumentException("Date format is wrong");
            }

            if (string.IsNullOrEmpty(code) || !ISO._4217.CurrencyCodesResolver.Codes.
                Any(c => c.Code == code.ToUpper()))
            {
                throw new ArgumentException("Currency code is wrong");
            }

            using var httpClient = this.clientFactory.CreateClient("NBP");
            HttpResponseMessage response = await httpClient.GetAsync(
                $"{this.averageRateTableName}/{code}/{date}/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<AverageRateList>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpRequestException("Not found", null, HttpStatusCode.NotFound);
            }
            else
            {
                throw new HttpRequestException("Error", null, response.StatusCode);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown if date or currency code is not valid.</exception>
        /// <exception cref="InvalidOperationException">Thrown if there are any problems with request/response
        /// to NBP API.</exception>
        public async Task<BuySellRatesList> GetMajorDifference(string code, int quotationNumber)
        {
            if (string.IsNullOrEmpty(code) || !ISO._4217.CurrencyCodesResolver.Codes.Any(c => c.Code == code.ToUpper()))
            {
                throw new ArgumentException("Currency code is wrong.");
            }

            if (quotationNumber > 255 || quotationNumber < 1)
            {
                throw new ArgumentException("Quotation number must not be in range 1 to 255.");
            }

            using var httpClient = this.clientFactory.CreateClient("NBP");
            HttpResponseMessage response = await httpClient.GetAsync(
                $"{this.buySellRatesTableName}/{code}/last/{quotationNumber}/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<BuySellRatesList>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpRequestException("Not found", null, HttpStatusCode.NotFound);
            }
            else
            {
                throw new HttpRequestException("Error", null, response.StatusCode);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown if currency code or qoutation number is not
        /// valid.</exception>
        /// <exception cref="InvalidOperationException">Thrown if there are any problems with request/response
        /// to NBP API.</exception>
        public async Task<AverageRateList> GetMaxMinExchangeRates(string code, int quotationNumber)
        {
            if (string.IsNullOrEmpty(code) || !ISO._4217.CurrencyCodesResolver.Codes.Any(c => c.Code == code.ToUpper()))
            {
               throw new ArgumentException("Currency code is wrong.");
            }

            if (quotationNumber > 255 || quotationNumber < 1)
            {
                throw new ArgumentException("Quotation number must not be in range 1 to 255.");
            }

            using var httpClient = this.clientFactory.CreateClient("NBP");
            HttpResponseMessage response = await httpClient.GetAsync(
                $"{this.averageRateTableName}/{code}/last/{quotationNumber}/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<AverageRateList>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpRequestException("Not found", null, HttpStatusCode.NotFound);
            }
            else
            {
                throw new HttpRequestException("Error", null, response.StatusCode);
            }
        }
    }
}
