using ShoppingBasket.Service.Common.Enums;

namespace ShoppingBasket.Service.Common.QueryParams
{
    /// <summary>
    /// Sorting params.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Common.QueryParams.ISortingParams" />
    internal class SortingParams : ISortingParams
    {
        /// <summary>
        /// Gets or sets the sort by.
        /// </summary>
        /// <value>
        /// The sort by.
        /// </value>
        public string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public SortOrder SortOrder { get; set; }
    }
}