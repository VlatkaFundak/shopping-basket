namespace ShoppingBasket.Service.Common.Filters
{
    /// <summary>
    ///   Base filter params contract
    /// </summary>
    public interface IBaseFilterParams
    {
        /// <summary>
        /// Gets or sets the ids.
        /// </summary>
        /// <value>The ids.</value>
        IEnumerable<Guid> Ids { get; set; }
    }
}