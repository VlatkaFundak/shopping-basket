using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service.Models.Products
{
    /// <summary>
    /// Product
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.BaseModel" />
    /// <seealso cref="ShoppingBasket.Service.Models.Products.Contracts.IProduct" />
    public class Product : BaseModel, IProduct
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the product category identifier.
        /// </summary>
        /// <value>
        /// The product category identifier.
        /// </value>
        public Guid ProductCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the discounts.
        /// </summary>
        /// <value>
        /// The discounts.
        /// </value>
        public IEnumerable<IDiscount> Discounts { get; set; }
    }
}