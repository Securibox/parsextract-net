namespace Securibox.ParseXtract.Models.Processing.Invoice
{
    /// <summary>
    /// Definition of ProcessedData for Invoice
    /// </summary>
    public class PostProcessedInvoice : ProcessedData
    {
        /// <summary>
        /// Invoice type
        /// </summary>
        public string Type;
        /// <summary>
        /// Invoice Number
        /// </summary>
        public string Number;
        /// <summary>
        /// Invoice OrderNumber
        /// </summary>
        public string OrderNumber;
        /// <summary>
        /// Invoice Emission Date
        /// </summary>
        public string Date_Emission;
        /// <summary>
        /// Invoice Amount information
        /// </summary>
        public Amount Amount;
        /// <summary>
        /// Invoice Supplier information
        /// </summary>
        public Entity Supplier;
        /// <summary>
        /// Invoice Customer information
        /// </summary>
        public Entity Customer;
        /// <summary>
        /// Invoice Due Date
        /// </summary>
        public string Date_Due;
    }
}
