namespace BankApp.API.Models.Rates
{
    /// <summary>
    /// Class representing  currency rate for a <see cref="Rate"/>.
    /// </summary>
    public class BuySellRate : Rate
    {
        /// <summary>
        /// Gets or sets the highest price a buyer will pay to buy a specified amount of currency
        /// for a <see cref="Rate"/>.
        /// </summary>
        required public decimal Bid { get; set; }

        /// <summary>
        /// Gets or sets the lowest price at which a seller will sell a specified amount of currency
        /// for a <see cref="Rate"/>.
        /// </summary>
        required public decimal Ask { get; set; }
    }
}
