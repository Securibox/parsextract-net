namespace Securibox.ParseXtract.Models.Processing.Invoice
{
    /// <summary>
    /// Description of an address like:
    /// 
    /// Stargate command 
    /// Cheyenne mountain   (otpional)
    /// Floor -32           (optional)
    /// 50000 Cheyenne
    /// US
    /// </summary>
    public class Address
    {
        /// <summary>
        /// First Line of the Address
        /// </summary>
        public string Line1;
        /// <summary>
        /// Second Line of the Address
        /// </summary>
        public string Line2;
        /// <summary>
        /// Third Line of the Address
        /// </summary>
        public string Line3;
        /// <summary>
        /// ZipCode of the city
        /// </summary>
        public string ZIPCode;
        /// <summary>
        /// City
        /// </summary>
        public string City;
        /// <summary>
        /// Country
        /// </summary>
        public string Country;
    }
}
