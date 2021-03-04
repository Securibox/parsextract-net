using Newtonsoft.Json;
using Securibox.ParseXtract.Models.JsonConverters;
using Securibox.ParseXtract.Models.Processing;
using System.Collections.Generic;

namespace Securibox.ParseXtract.Models
{
    /// <summary>
    /// Post processed result containing data and errors
    /// </summary>
    public class PostProcessedResult
    {
        /// <summary>
        /// Post Processed data container
        /// </summary>
        [JsonConverter(typeof(ProcessedDataJsonConverter))]
        public ProcessedData Data;

        /// <summary>
        /// List of errors grouped by ErrorType
        /// </summary>
        public IDictionary<ErrorType, List<string>> Errors;

        /// <summary>
        /// Maximal error serverity encounterd
        /// </summary>
        public ErrorLevel ErrorLevel;
    }
}
