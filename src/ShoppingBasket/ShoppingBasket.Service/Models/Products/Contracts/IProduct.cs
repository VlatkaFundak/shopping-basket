using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Products.Contracts
{
    /// <summary>
    /// Product contract
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.IBaseModel" />
    public interface IProduct : IBaseModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the product category identifier.
        /// </summary>
        /// <value>
        /// The product category identifier.
        /// </value>
        Guid ProductCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the discounts.
        /// </summary>
        /// <value>
        /// The discounts.
        /// </value>
        IEnumerable<IDiscount> Discounts { get; set; }
    }
}