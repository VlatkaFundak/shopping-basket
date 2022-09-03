using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Discounts
{
    public class ProductExtraQuantityDiscount : Discount, IProductExtraQuantityDiscount
    {
        public int MainQuantity { get; set; }

        public int DiscountQuantity { get; set; }
    }
}