namespace ShoppingBasket.Service.Common.QueryParams
{
    /// <summary>
    /// Sorting params contract.
    /// </summary>
    public interface ISortingParams
    {
        /// <summary>
        /// Gets or sets the sort by.
        /// </summary>
        /// <value>
        /// The sort by.
        /// </value>
        string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        SortOrder SortOrder { get; set; }
    }
}