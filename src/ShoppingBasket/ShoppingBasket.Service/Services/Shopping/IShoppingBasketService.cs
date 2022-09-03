using ShoppingBasket.Service.Models;

namespace ShoppingBasket.Service.Services.Shopping
{
    public interface IShoppingBasketService
    {
        Task CalculateBasketTotalAsync(IShoppingBasket shoppingBasket);

        Task<IShoppingBasket> CreateShoppingBasketAsync(string userIdentifier);
    }
}