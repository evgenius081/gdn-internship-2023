using BankApp.API.Models.RateLists;

namespace BankApp.API.Interfaces
{
    /// <summary>
    /// Interface for service dealing with NBP API.
    /// </summary>
    public interface IBankService
    {
        /// <summary>
        /// Gets average exchange rate to PLN for given date and currency from NBP API.
        /// </summary>
        /// <param name="date">Date in ISO 8601 standard.</param>
        /// <param name="code">Currency code in ISO 4217 standard.</param>
        /// <returns>Task with <see cref="RateList"/> object containing currency rate info.</returns>
        public Task<AverageRateList> GetAverageExchangeRate(string date, string code);

        /// <summary>
        /// Gets min and max exchange rates to PLN from given range for given currency from NBP API.
        /// </summary>
        /// <param name="code">Currency code in ISO 4217 standard.</param>
        /// <param name="quotationNumber">Number of quotationts to get.</param>
        /// <returns>Tuple with min and max exchange rates within the quotation number range.</returns>
        public Task<AverageRateList> GetMaxMinExchangeRates(string code, int quotationNumber);

        /// <summary>
        /// Gets major difference between all sell and buy rates to PLN in given range for given currency 
        /// from NBP API.
        /// </summary>
        /// <param name="code">Currency code in ISO 4217 standard.</param>
        /// <param name="quotationNumber">Number of quotationts to get.</param>
        /// <returns>Major difference between all buy and sell rates to PLN.</returns>
        public Task<BuySellRatesList> GetMajorDifference(string code, int quotationNumber);
    }
}
