using ShoppingBasket.Service.Models;

namespace ShoppingBasket.Service.Infrastructure
{
    internal interface IShoppingBasketLogger : ILogger
    {
        Task LogTotalAsync(IShoppingBasket shoppingBasket);
    }
}