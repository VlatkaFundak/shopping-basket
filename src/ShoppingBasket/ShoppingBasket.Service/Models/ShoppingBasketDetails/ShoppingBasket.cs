using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Models.ShoppingBasketDetails
{
    /// <summary>
    /// Shopping basket
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.BaseModel" />
    /// <seealso cref="ShoppingBasket.Service.Models.IShoppingBasket" />
    internal class ShoppingBasket : BaseModel, IShoppingBasket
    {
        /// <summary>
        /// Gets or sets the shopping basket items.
        /// </summary>
        /// <value>
        /// The shopping basket items.
        /// </value>
        public IEnumerable<IShoppingBasketItem> ShoppingBasketItems { get; set; }

        /// <summary>
        /// User identifier that groups shopping basket by unlogged user
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the discounts.
        /// </summary>
        /// <value>
        /// The discounts.
        /// </value>
        public IEnumerable<IDiscount> Discounts { get; set; }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal Total { get; set; }
    }
}