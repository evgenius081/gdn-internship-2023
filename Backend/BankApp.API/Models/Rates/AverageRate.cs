namespace BankApp.API.Models.Rates
{
    /// <summary>
    /// Class representing average currency rate for a <see cref="Rate"/>.
    /// </summary>
    public class AverageRate : Rate
    {
        /// <summary>
        /// Gets or sets the currency rate to PLN.
        /// </summary>
        public decimal Mid { get; set; }
    }
}
