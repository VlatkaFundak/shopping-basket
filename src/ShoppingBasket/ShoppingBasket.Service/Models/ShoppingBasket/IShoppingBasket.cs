using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models
{
    /// <summary>
    /// Shopping basket contract
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.IBaseModel" />
    public interface IShoppingBasket : IBaseModel
    {
        /// <summary>
        /// User identifier that groups shopping basket by unlogged user
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        string UserIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the discounts.
        /// </summary>
        /// <value>
        /// The discounts.
        /// </value>
        IEnumerable<IDiscount> Discounts { get; set; }

        /// <summary>
        /// Gets or sets the shopping basket items.
        /// </summary>
        /// <value>
        /// The shopping basket items.
        /// </value>
        IEnumerable<IShoppingBasketItem> ShoppingBasketItems { get; set; }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        decimal Total { get; internal set; }
    }
}