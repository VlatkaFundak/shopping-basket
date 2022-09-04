using ShoppingBasket.Service.Models;
using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service
{
    /// <summary>
    /// Shopping basket item contract
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.IBaseModel" />
    public interface IShoppingBasketItem : IBaseModel
    {
        /// <summary>
        /// Gets or sets the shopping basket identifier.
        /// </summary>
        /// <value>
        /// The shopping basket identifier.
        /// </value>
        Guid ShoppingBasketId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        IProduct Product { get; set; }
    }
}