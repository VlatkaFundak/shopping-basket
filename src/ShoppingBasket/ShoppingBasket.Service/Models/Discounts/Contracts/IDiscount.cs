using ShoppingBasket.Service.Common.Enums;

namespace ShoppingBasket.Service.Models.Discounts.Contracts
{
    /// <summary>
    /// Discount contract
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.IBaseModel" />
    public interface IDiscount : IBaseModel
    {
        /// <summary>
        /// Gets or sets the type of the discount.
        /// </summary>
        /// <value>
        /// The type of the discount.
        /// </value>
        DiscountType DiscountType { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [exclude other discounts].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exclude other discounts]; otherwise, <c>false</c>.
        /// </value>
        bool ExcludeOtherDiscounts { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the main product identifier.
        /// </summary>
        /// <value>
        /// The main product identifier.
        /// </value>
        Guid MainProductId { get; set; }
    }
}