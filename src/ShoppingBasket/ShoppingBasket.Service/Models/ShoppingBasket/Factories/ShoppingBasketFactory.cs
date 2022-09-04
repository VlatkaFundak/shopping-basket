namespace ShoppingBasket.Service.Models.Factories
{
    /// <summary>
    /// Shopping basket factory
    /// </summary>
    internal static class ShoppingBasketFactory
    {
        /// <summary>
        /// Creates the shopping basket.
        /// </summary>
        /// <returns></returns>
        internal static IShoppingBasket CreateShoppingBasket()
        {
            return new ShoppingBasket();
        }
    }
}