namespace Securibox.ParseXtract.Models.Processing
{
    /// <summary>
    /// Define error level of severity
    /// </summary>
    public enum ErrorLevel
    {
        /// <summary>
        /// No Error
        /// </summary>
        None = 0,
        /// <summary>
        /// Notification
        /// </summary>
        Notice = 1,
        /// <summary>
        /// Warning
        /// </summary>
        Warning = 2,
        /// <summary>
        /// Constitutive error
        /// </summary>
        Error = 3,
        /// <summary>
        /// Structural error
        /// </summary>
        Pass = 4,
    }
}
