using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankApp.API.Models.Rates
{
    /// <summary>
    /// Class representing currency rate to PLN.
    /// </summary>
    [JsonDerivedType(typeof(AverageRate), typeDiscriminator: nameof(AverageRate))]
    public abstract class Rate
    {
        /// <summary>
        /// Gets or sets the number of currency rate in table where the rate was taken from.
        /// </summary>
        public string? No { get; set; }

        /// <summary>
        /// Gets or sets the currency rate publication date.
        /// </summary>
        public string? EffectiveDate { get; set; }
    }
}
