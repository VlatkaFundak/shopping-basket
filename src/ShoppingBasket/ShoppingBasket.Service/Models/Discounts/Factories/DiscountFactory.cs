using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Discounts.Factories
{
    /// <summary>
    /// Discount factory.
    /// </summary>
    public static class DiscountFactory
    {
        /// <summary>
        /// Creates the discount.
        /// </summary>
        /// <returns></returns>
        public static IDiscount CreateDiscount()
        {
            return new Discount();
        }
    }
}