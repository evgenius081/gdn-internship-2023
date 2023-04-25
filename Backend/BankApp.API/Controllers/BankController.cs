using System.Globalization;
using BankApp.API.Interfaces;
using BankApp.API.Models.Rates;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.API.Controllers
{
    /// <summary>
    /// Controller handling requests for currency data from NBP.
    /// </summary>
    [Route("api/bank")]
    public class BankController : Controller
    {
        private readonly IBankService bankService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankController"/> class.
        /// </summary>
        /// <param name="bankService">Service implementing <see cref="IBankService"/> interface.</param>
        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        /// <summary>
        /// Handles request for getting the average currency rate to PLN for given date and currency.
        /// </summary>
        /// <param name="date">Date in ISO 8601 standard.</param>
        /// <param name="code">Currency code in ISO 4217 standard.</param>
        /// <returns><see cref="Task{IActionResult}"/> with Ok response containing decimal average exchange
        /// rate to PLN for given date and currency code, or <see cref="Task{IActionResult}"/> with Bad Request
        /// or Not found responses if request had error or the NBP API is down.</returns>
        [HttpGet]
        [Route("average/{date}/{code}")]
        public async Task<IActionResult> AverageExchangeRate(string date, string code)
        {
            try
            {
                var rateList = await this.bankService.GetAverageExchangeRate(date, code);
                if (rateList == null || rateList.Rates == null || rateList.Rates.Count == 0)
                {
                    return this.BadRequest();
                }

                return this.Ok(((AverageRate)rateList.Rates[0]).Mid);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return this.NotFound(ex.Message);
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return this.BadRequest("National Polish Bank is unavailbale");
                }

                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Handles request for getting min and max currency rate to PLN within the quotation number for given
        /// currency.
        /// </summary>
        /// <param name="code">Currency code in ISO 4217 standard.</param>
        /// <param name="quotationNumber">Quotation number to be reviewed.</param>
        /// <returns><see cref="Task{IActionResult}"/> with Ok response containing tuple with min and max
        /// currency rates within the given range, or <see cref="Task{IActionResult}"/> with Bad Request
        /// or Not found responses if request had error or the NBP API is down.</returns>
        [HttpGet]
        [Route("minmax/{code}/{quotationNumber}")]
        public async Task<IActionResult> MinMaxExchangeRates(string code, int quotationNumber)
        {
            try
            {
                var rateList = await this.bankService.GetMaxMinExchangeRates(code, quotationNumber);
                if (rateList == null || rateList.Rates == null || rateList.Rates.Count == 0)
                {
                    return this.BadRequest();
                }

                var rates = rateList.Rates.Select(rate => rate.Mid);

                return this.Ok(new Tuple<decimal, decimal>(rates.Min(), rates.Max()));
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return this.NotFound(ex.Message);
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return this.BadRequest("National Polish Bank is unavailbale");
                }

                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Handles request for getting the major difference between currency buy and sell rates to PLN
        /// within the quotation number for given currency.
        /// </summary>
        /// <param name="code">Currency code in ISO 4217 standard.</param>
        /// <param name="quotationNumber">Quotation number to be reviewed.</param>
        /// <returns><see cref="Task{IActionResult}"/> with Ok response containing
        /// <see cref="decimal"/> with major difference between sell and buy currency rates within the given
        /// range, or <see cref="Task{IActionResult}"/> with Bad Request or Not found responses if request had
        /// error or the NBP API is down.</returns>
        [HttpGet]
        [Route("majordiff/{code}/{quotationNumber}")]
        public async Task<IActionResult> MajorDifference(string code, int quotationNumber)
        {
            try
            {
                var rateList = await this.bankService.GetMajorDifference(code, quotationNumber);
                if (rateList == null || rateList.Rates == null || rateList.Rates.Count == 0)
                {
                    return this.BadRequest();
                }

                var rateDifferences = rateList.Rates.Select(rate => rate.Ask - rate.Bid);

                return this.Ok(rateDifferences.Max());
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return this.NotFound(ex.Message);
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return this.BadRequest("National Polish Bank is unavailbale");
                }

                return this.BadRequest(ex.Message);
            }
        }
    }
}
