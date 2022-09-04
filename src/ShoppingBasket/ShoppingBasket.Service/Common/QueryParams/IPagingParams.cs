namespace ShoppingBasket.Service.Common.QueryParams
{
    /// <summary>
    /// Paging params contract.
    /// </summary>
    public interface IPagingParams
    {
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        int PageSize { get; set; }
    }
}