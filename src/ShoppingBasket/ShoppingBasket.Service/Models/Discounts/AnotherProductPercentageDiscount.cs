using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Discounts
{
    /// <summary>
    /// Main product percentage discount for another product.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.Discounts.Discount" />
    /// <seealso cref="ShoppingBasket.Service.Models.Discounts.Contracts.IAnotherProductPercentageDiscount" />
    internal class AnotherProductPercentageDiscount : Discount, IAnotherProductPercentageDiscount
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

        /// <summary>
        /// Gets or sets the discount product identifier.
        /// </summary>
        /// <value>
        /// The discount product identifier.
        /// </value>
        public Guid DiscountProductId { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public int Percentage { get; set; }
    }
}