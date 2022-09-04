namespace ShoppingBasket.Service.Common.Filters
{
    /// <summary>
    /// Shopping basket item filter params.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Common.Filters.BaseFilterParams" />
    /// <seealso cref="ShoppingBasket.Service.Common.Filters.IShoppingBasketItemFilterParams" />
    public class ShoppingBasketItemFilterParams : BaseFilterParams, IShoppingBasketItemFilterParams
    {
        /// <summary>
        /// Gets or sets the shopping basket identifier.
        /// </summary>
        /// <value>
        /// The shopping basket identifier.
        /// </value>
        public Guid ShoppingBasketId { get; set; }
    }
}