using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Discounts.Enums;

namespace ShoppingBasket.Service.Providers
{
    public interface IDiscountProvider
    {
        DiscountType DiscountType { get; }

        Task<decimal> GetDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts);
    }
}