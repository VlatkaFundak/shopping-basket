namespace ShoppingBasket.Service.Common.QueryParams
{
    /// <summary>
    /// Sorting params factory.
    /// </summary>
    public static class SortingParamsFactory
    {
        /// <summary>
        /// Creates the sorting parameters.
        /// </summary>
        /// <returns></returns>
        public static ISortingParams CreateSortingParams()
        {
            return new SortingParams();
        }
    }
}