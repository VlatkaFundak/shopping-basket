namespace ShoppingBasket.Service.Common.QueryParams
{
    /// <summary>
    /// Paging params factory.
    /// </summary>
    public static class PagingParamsFactory
    {
        /// <summary>
        /// Creates the paging parameters.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static IPagingParams CreatePagingParams(int pageNumber = 10, int pageSize = 1)
        {
            return new PagingParams(pageNumber, pageSize);
        }
    }
}