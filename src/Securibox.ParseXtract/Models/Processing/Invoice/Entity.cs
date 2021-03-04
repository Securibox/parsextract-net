namespace Securibox.ParseXtract.Models.Processing.Invoice
{
    /// <summary>
    /// Descritpion of entity information
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Entity Name
        /// </summary>
        public string Name;
        /// <summary>
        /// Entity National Identifier
        /// </summary>
        public string SIREN;
        /// <summary>
        /// Entity Tax Identifier
        /// </summary>
        public string VAT;
        /// <summary>
        /// Entity Address
        /// </summary>
        public Address Address;
        /// <summary>
        /// Entity Detailled National Identifier
        /// </summary>
        public string SIRET;
    }
}
