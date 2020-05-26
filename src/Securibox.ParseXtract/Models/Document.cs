namespace Securibox.ParseXtract.Models
{
    /// <summary>
    /// Represents a document ot be parsed
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Document name or identifier
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Document content base 64 encoded
        /// </summary>
        public string Base64Content { get; set; }
    }
}
