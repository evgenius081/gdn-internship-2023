using BankApp.API.Models.Rates;

namespace BankApp.API.Models.RateLists
{
    /// <summary>
    /// Class representing the list of <see cref="AverageRate"/> objects.
    /// </summary>
    public class AverageRateList : RateList
    {
        /// <summary>
        /// Gets or sets the list of <see cref="AverageRate"/> objects.
        /// </summary>
        required public List<AverageRate> Rates { get; set; }
    }
}
