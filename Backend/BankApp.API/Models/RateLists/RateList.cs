namespace BankApp.API.Models.RateLists
{
    /// <summary>
    /// Abstract class representing the table from api.nbp.pl.
    /// </summary>
    public class RateList
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        public string? Table { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// Gets or sets the ISO 4217 code of the currency.
        /// </summary>
        public string? Code { get; set; }
    }
}
