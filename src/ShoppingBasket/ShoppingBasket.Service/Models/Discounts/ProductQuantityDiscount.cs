using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Discounts
{
    /// <summary>
    /// Product quantity discount for main product.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.Discounts.Discount" />
    /// <seealso cref="ShoppingBasket.Service.Models.Discounts.Contracts.IProductQuantityDiscount" />
    public class ProductQuantityDiscount : Discount, IProductQuantityDiscount
    {
        /// <summary>
        /// Gets or sets the main quantity.
        /// </summary>
        /// <value>
        /// The main quantity.
        /// </value>
        public int MainQuantity { get; set; }

        /// <summary>
        /// Gets or sets the discount quantity.
        /// </summary>
        /// <value>
        /// The discount quantity.
        /// </value>
        public int DiscountQuantity { get; set; }
    }
}