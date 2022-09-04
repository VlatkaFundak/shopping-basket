namespace ShoppingBasket.Service.Models.Discounts.Contracts
{
    /// <summary>
    /// Product quantity discount contract.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.Discounts.Contracts.IDiscount" />
    public interface IProductQuantityDiscount : IDiscount
    {
        /// <summary>
        /// Gets or sets the discount quantity.
        /// </summary>
        /// <value>
        /// The discount quantity.
        /// </value>
        int DiscountQuantity { get; set; }

        /// <summary>
        /// Gets or sets the main quantity.
        /// </summary>
        /// <value>
        /// The main quantity.
        /// </value>
        int MainQuantity { get; set; }
    }
}