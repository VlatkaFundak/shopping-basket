using ShoppingBasket.Service.Models;
using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service
{
    /// <summary>
    /// Shopping basket item
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.BaseModel" />
    /// <seealso cref="ShoppingBasket.Service.IShoppingBasketItem" />
    public class ShoppingBasketItem : BaseModel, IShoppingBasketItem
    {
        /// <summary>
        /// Gets or sets the shopping basket identifier.
        /// </summary>
        /// <value>
        /// The shopping basket identifier.
        /// </value>
        public Guid ShoppingBasketId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public IProduct Product { get; set; }
    }
}