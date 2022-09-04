namespace ShoppingBasket.Service.Infrastructure.Factories
{
    /// <summary>
    /// Logger factory
    /// </summary>
    internal static class LoggerFactory
    {
        /// <summary>
        /// Creates the shopping logger.
        /// </summary>
        /// <returns></returns>
        internal static IShoppingBasketLogger CreateShoppingLogger()
        {
            return new ShoppingBasketLogger();
        }
    }
}