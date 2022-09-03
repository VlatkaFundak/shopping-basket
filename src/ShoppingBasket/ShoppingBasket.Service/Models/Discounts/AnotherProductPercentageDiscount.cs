using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Discounts
{
    public class AnotherProductPercentageDiscount : Discount, IAnotherProductPercentageDiscount
    {
        public int MainQuantity { get; set; }

        public int DiscountQuantity { get; set; }

        public Guid DiscountProductId { get; set; }

        public int Percentage { get; set; }
    }
}