using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts
{
    /// <summary>
    /// Basket calculation service contract
    /// </summary>
    internal interface IBasketCalculationService
    {
        /// <summary>
        /// Calculates the total asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        Task<decimal> CalculateTotalAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts);
    }
}