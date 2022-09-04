using ShoppingBasket.Service.Common.Filters;
using ShoppingBasket.Service.Common.QueryParams;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts
{
    /// <summary>
    /// Shopping basket item service contract
    /// </summary>
    public interface IShoppingBasketItemService
    {
        /// <summary>
        /// Adds the shopping basket item asynchronous.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        Task AddShoppingBasketItemAsync(IEnumerable<IShoppingBasketItem> items);

        /// <summary>
        /// Creates the shopping basket item asynchronous.
        /// </summary>
        /// <param name="shoppingBasketId">The shopping basket identifier.</param>
        /// <returns></returns>
        Task<IShoppingBasketItem> CreateShoppingBasketItemAsync(Guid shoppingBasketId);

        /// <summary>
        /// Deletes the shopping basket item asynchronous.
        /// </summary>
        /// <param name="itemIds">The item ids.</param>
        /// <returns></returns>
        Task DeleteShoppingBasketItemAsync(IEnumerable<Guid> itemIds);

        /// <summary>
        /// Finds the shopping basket item asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="pagingParams">The paging parameters.</param>
        /// <param name="sortingParams">The sorting parameters.</param>
        /// <returns></returns>
        Task<IEnumerable<IShoppingBasketItem>> FindShoppingBasketItemAsync(IShoppingBasketItemFilterParams filter, IPagingParams pagingParams, ISortingParams sortingParams);
    }
}