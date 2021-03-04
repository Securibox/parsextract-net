using System.Collections.Generic;

namespace Securibox.ParseXtract.Models
{
    /// <summary>
    /// Represents the result of a parsing session
    /// </summary>
    public class ParsingResult
    {
        /// <summary>
        /// Document name or identifier
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Collection of data extracted from the document
        /// </summary>
        public IEnumerable<XData> XData { get; set; }

        /// <summary>
        /// Result of post processing of XData
        /// </summary>
        public PostProcessedResult PostProccessedResult { get; set; }
    }
}
