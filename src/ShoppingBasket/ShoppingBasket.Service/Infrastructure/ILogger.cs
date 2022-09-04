namespace ShoppingBasket.Service.Infrastructure
{
    /// <summary>
    /// Logger contract.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        void LogError(string message, Exception ex);
    }
}