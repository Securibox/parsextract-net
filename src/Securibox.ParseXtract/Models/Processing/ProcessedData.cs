namespace Securibox.ParseXtract.Models.Processing
{
    /// <summary>
    /// Abstract class to define the data object rendered by the selected postprocessor
    /// </summary>
    public abstract class ProcessedData 
    {
        /// <summary>
        /// ProcessedType type
        /// </summary>
        public string ProcessedType { get { return this.GetType().Name; } }
    }
}