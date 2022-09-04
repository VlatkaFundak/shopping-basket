namespace ShoppingBasket.Service.Common.QueryParams
{
    /// <summary>
    /// Paging params.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Common.QueryParams.IPagingParams" />
    internal class PagingParams : IPagingParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagingParams"/> class.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagingParams(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; }
    }
}