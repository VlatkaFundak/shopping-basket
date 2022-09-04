using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service.Models.Products
{
    /// <summary>
    /// Product category
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.BaseModel" />
    /// <seealso cref="ShoppingBasket.Service.Models.Products.Contracts.IProductCategory" />
    internal class ProductCategory : BaseModel, IProductCategory
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abrv.
        /// </summary>
        /// <value>
        /// The abrv.
        /// </value>
        public string Abrv { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}