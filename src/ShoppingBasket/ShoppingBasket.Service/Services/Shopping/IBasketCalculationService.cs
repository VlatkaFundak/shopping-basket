using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Services.Shopping
{
    public interface IBasketCalculationService
    {
        Task<decimal> CalculateTotalAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts);
    }
}