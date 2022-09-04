using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Models.ShoppingBasketDetails.Factories
{
    /// <summary>
    /// Shopping basket item factory
    /// </summary>
    internal static class ShoppingBasketItemFactory
    {
        /// <summary>
        /// Creates the shopping basket item.
        /// </summary>
        /// <returns></returns>
        internal static IShoppingBasketItem CreateShoppingBasketItem()
        {
            return new ShoppingBasketItem();
        }
    }
}