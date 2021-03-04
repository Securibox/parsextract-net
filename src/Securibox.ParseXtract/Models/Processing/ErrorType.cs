namespace Securibox.ParseXtract.Models.Processing
{
    /// <summary>
    /// Enumeration of error type
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Field has been Reconstructed
        /// </summary>
        FieldReconstructed = 0,
        /// <summary>
        /// Field has not been Validated
        /// </summary>
        FieldNotValidated = 1,
        /// <summary>
        /// External Api used returned an error
        /// </summary>
        ApiError = 2,

        /// <summary>
        /// Field has not been Extracted
        /// </summary>
        FieldNotExtracted = 3,
        /// <summary>
        /// Field has not been Parsed
        /// </summary>
        FieldNotParsed = 4,
        /// <summary>
        /// Field met conflict with some reconstruction rule
        /// </summary>
        Conflict = 5,

        /// <summary>
        /// Field has a invalid value (e.g Vat = 0)
        /// </summary>
        FieldInvalid = 6,

        /// <summary>
        /// Field has a not supported value
        /// </summary>
        NotSupported = 7,
        /// <summary>
        /// A Mandatory Condition is not respected
        /// </summary>
        MandatoryConditionFailed = 8,
    }
}
