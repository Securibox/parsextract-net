namespace Securibox.ParseXtract.Models.Processing.Invoice
{
    /// <summary>
    /// Description of amount data
    /// </summary>
    public class Amount
    {
        /// <summary>
        /// Amount Net
        /// </summary>
        public decimal? Net;
        /// <summary>
        /// Amount Tax
        /// </summary>
        public decimal? Tax;
        /// <summary>
        /// Amount Total
        /// </summary>
        public decimal? Total;
        /// <summary>
        /// Exchange Rate to EUR if currency not EUR
        /// </summary>
        public decimal? ExchangeRate;
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency;
    }
}
