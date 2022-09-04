namespace ShoppingBasket.Service.Infrastructure
{
    /// <summary>
    /// File logger.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Infrastructure.ILogger" />
    public class FileLogger : ILogger //log4net usually
    {
        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInfo(string message)
        {
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void LogError(string message, Exception ex)
        {
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogError(string message)
        {
        }
    }
}