namespace ShoppingBasket.Service.Common.Filters
{
    /// <summary>
    /// Shopping basket item filter params contract
    /// </summary>
    public interface IShoppingBasketItemFilterParams : IBaseFilterParams
    {
        /// <summary>
        /// Gets or sets the shopping basket identifier.
        /// </summary>
        /// <value>The shopping basket identifier.</value>
        Guid ShoppingBasketId { get; set; }
    }
}