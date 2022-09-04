using ShoppingBasket.Infrastructure.Service.Infrastructure.Extensions;
using ShoppingBasket.Service.Common.Filters;
using ShoppingBasket.Service.Common.QueryParams;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Factories;
using ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Services.ShoppingBasketDetails
{
    /// <summary>
    /// Shopping basket item service
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Services.BaseService" />
    /// <seealso cref="ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts.IShoppingBasketItemService" />
    public class ShoppingBasketItemService : BaseService, IShoppingBasketItemService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingBasketItemService"/> class.
        /// </summary>
        public ShoppingBasketItemService() //TODO implement inject ShoppingBasketRepository for CRUD operations from database
        {
            _items = new HashSet<IShoppingBasketItem>();
        }

        /// <summary>
        /// The items
        /// </summary>
        private HashSet<IShoppingBasketItem> _items;

        /// <summary>
        /// Deletes the shopping basket item asynchronous.
        /// </summary>
        /// <param name="itemIds">The item ids.</param>
        /// <returns></returns>
        public Task DeleteShoppingBasketItemAsync(IEnumerable<Guid> itemIds)
        {
            _items = _items.Where(p => !itemIds.Contains(p.Id)).ToHashSet();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates the shopping basket item asynchronous.
        /// </summary>
        /// <param name="shoppingBasketId">The shopping basket identifier.</param>
        /// <returns></returns>
        public async Task<IShoppingBasketItem> CreateShoppingBasketItemAsync(Guid shoppingBasketId)
        {
            var item = ShoppingBasketItemFactory.CreateShoppingBasketItem();
            item.ShoppingBasketId = shoppingBasketId;

            await InitializeAsync(item);
            return item;
        }

        /// <summary>
        /// Finds the shopping basket item asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="pagingParams">The paging parameters.</param>
        /// <param name="sortingParams">The sorting parameters.</param>
        /// <returns></returns>
        public async Task<IEnumerable<IShoppingBasketItem>> FindShoppingBasketItemAsync(IShoppingBasketItemFilterParams filter, IPagingParams pagingParams, ISortingParams sortingParams)
        {
            var result = await OnFindShoppingBasketFilterAsync(filter);
            return result;
        }

        /// <summary>
        /// Called when [find shopping basket filter asynchronous].
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        protected Task<IEnumerable<IShoppingBasketItem>> OnFindShoppingBasketFilterAsync(IShoppingBasketItemFilterParams filter)
        {
            var items = new List<IShoppingBasketItem>();
            if (filter is not null)
            {
                if (filter.Ids is not null && filter.Ids.Any())
                {
                    items = items.Concat(_items.Where(p => filter.Ids.Contains(p.Id)).ToList()).ToList();
                }

                if (!GuidExtensions.IsNullOrEmpty(filter.ShoppingBasketId))
                {
                    items = items.Concat(_items.Where(p => filter.ShoppingBasketId.Equals(p.ShoppingBasketId)).ToList()).ToList();
                }
            }

            return Task.FromResult(items.AsEnumerable());
        }

        /// <summary>
        /// Adds the shopping basket item asynchronous.
        /// </summary>
        /// <param name="items">The items.</param>
        public async Task AddShoppingBasketItemAsync(IEnumerable<IShoppingBasketItem> items)
        {
            foreach (var item in items)
            {
                await InitializeAsync(item);
                _items.Add(item);
            }
        }
    }
}