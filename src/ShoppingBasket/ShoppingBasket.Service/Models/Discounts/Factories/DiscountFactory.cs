using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Discounts.Factories
{
    public class DiscountFactory : IDiscountFactory
    {
        public IDiscount CreateDiscount()
        {
            return new Discount();
        }
    }
}