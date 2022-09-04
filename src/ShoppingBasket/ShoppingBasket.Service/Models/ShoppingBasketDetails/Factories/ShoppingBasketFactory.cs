using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Models.ShoppingBasketDetails.Factories
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