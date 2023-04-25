using BankApp.API.Models.Rates;

namespace BankApp.API.Models.RateLists
{
    /// <summary>
    /// Class representing the list of <see cref="BuySellRate"/> objects.
    /// </summary>
    public class BuySellRatesList : RateList
    {
        /// <summary>
        /// Gets or sets the list of <see cref="BuySellRate"/> objects.
        /// </summary>
        required public List<BuySellRate> Rates { get; set; }
    }
}
