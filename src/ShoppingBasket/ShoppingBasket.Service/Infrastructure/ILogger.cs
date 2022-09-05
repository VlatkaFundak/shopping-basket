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
        /// <returns></returns>
        Task LogInfoAsync(string message);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        Task LogErrorAsync(string message, Exception ex);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task LogErrorAsync(string message);
    }
}