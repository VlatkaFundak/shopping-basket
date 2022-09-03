using ShoppingBasket.Service.Common.Filters;

namespace ShoppingBasket.Service.Services.Shopping
{
    public interface IShoppingBasketItemService
    {
        Task AddShoppingBasketItemAsync(IEnumerable<IShoppingBasketItem> items);

        Task<IShoppingBasketItem> CreateShoppingBasketItemAsync(Guid shoppingBasketId);

        Task DeleteShoppingBasketItemAsync(IEnumerable<Guid> itemIds);

        Task<IEnumerable<IShoppingBasketItem>> FindShoppingBasketItemAsync(IShoppingBasketItemFilterParams filter);
    }
}