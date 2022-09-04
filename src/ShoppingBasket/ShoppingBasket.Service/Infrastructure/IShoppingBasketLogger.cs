using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Infrastructure
{
    /// <summary>
    /// Shopping basket logger contract
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Infrastructure.ILogger" />
    internal interface IShoppingBasketLogger : ILogger
    {
        /// <summary>
        /// Logs the total asynchronous.
        /// </summary>
        /// <param name="shoppingBasket">The shopping basket.</param>
        /// <returns></returns>
        Task LogTotalAsync(IShoppingBasket shoppingBasket);
    }
}