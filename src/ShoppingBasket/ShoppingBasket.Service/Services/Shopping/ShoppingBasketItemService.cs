using ShoppingBasket.Service.Common.Filters;
using ShoppingBasket.Service.Models.Factories;

namespace ShoppingBasket.Service.Services.Shopping
{
    public class ShoppingBasketItemService : BaseService, IShoppingBasketItemService
    {
        public ShoppingBasketItemService() //TODO inject ShoppingBasketRepository for fetching items from database
        {
            _items = new HashSet<IShoppingBasketItem>();
        }

        private HashSet<IShoppingBasketItem> _items;

        public Task DeleteShoppingBasketItemAsync(IEnumerable<Guid> itemIds)
        {
            _items = _items.Where(p => !itemIds.Contains(p.Id)).ToHashSet();

            return Task.CompletedTask;
        }

        public async Task<IShoppingBasketItem> CreateShoppingBasketItemAsync(Guid shoppingBasketId)
        {
            var item = ShoppingBasketItemFactory.CreateShoppingBasketItem();
            item.ShoppingBasketId = shoppingBasketId;

            await InitializeAsync(item);
            return item;
        }

        public Task<IEnumerable<IShoppingBasketItem>> FindShoppingBasketItemAsync(IShoppingBasketItemFilterParams filter)
        {
            return Task.FromResult(_items.AsEnumerable());
        }

        public async Task AddShoppingBasketItemAsync(IEnumerable<IShoppingBasketItem> items)
        {
            foreach (var item in items)
            {
                await InitializeAsync(item);
                _items.Add(item); // TODO call to database to save item
            }
        }
    }
}