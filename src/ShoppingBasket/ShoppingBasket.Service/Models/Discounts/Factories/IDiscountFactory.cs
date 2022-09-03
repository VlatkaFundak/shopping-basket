using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Discounts.Factories
{
    public interface IDiscountFactory
    {
        IDiscount CreateDiscount();
    }
}