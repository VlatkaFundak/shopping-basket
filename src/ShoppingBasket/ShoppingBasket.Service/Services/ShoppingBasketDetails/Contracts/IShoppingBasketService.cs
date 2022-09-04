using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts
{
    /// <summary>
    /// Shopping basket service contract
    /// </summary>
    public interface IShoppingBasketService
    {
        /// <summary>
        /// Calculates the basket total asynchronous.
        /// </summary>
        /// <param name="shoppingBasket">The shopping basket.</param>
        /// <returns></returns>
        Task CalculateBasketTotalAsync(IShoppingBasket shoppingBasket);

        /// <summary>
        /// Creates the shopping basket asynchronous.
        /// </summary>
        /// <param name="userIdentifier">The user identifier.</param>
        /// <returns></returns>
        Task<IShoppingBasket> CreateShoppingBasketAsync(string userIdentifier);
    }
}