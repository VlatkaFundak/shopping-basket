namespace ShoppingBasket.Service.Common.Filters
{
    /// <summary>
    ///   Base filter params.
    /// </summary>
    public class BaseFilterParams : IBaseFilterParams
    {
        /// <summary>Gets or sets the ids.</summary>
        /// <value>The ids.</value>
        public IEnumerable<Guid> Ids { get; set; }
    }
}