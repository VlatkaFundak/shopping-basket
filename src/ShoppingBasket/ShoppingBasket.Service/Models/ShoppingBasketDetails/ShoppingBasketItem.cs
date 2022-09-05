using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Products.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Models.ShoppingBasketDetails
{
    /// <summary>
    /// Shopping basket item
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts.IShoppingBasketItem" />
    /// <seealso cref="ShoppingBasket.Service.Models.BaseModel" />
    /// <seealso cref="ShoppingBasket.Service.IShoppingBasketItem" />
    internal class ShoppingBasketItem : BaseModel, IShoppingBasketItem
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

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public IDiscount Discount { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        /// <value>
        /// The discount amount.
        /// </value>
        public decimal DiscountAmount { get; set; }
    }
}