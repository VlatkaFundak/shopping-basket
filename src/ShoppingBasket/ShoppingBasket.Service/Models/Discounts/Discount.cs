using ShoppingBasket.Service.Common.Enums;
using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Discounts
{
    /// <summary>
    /// Discount.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.BaseModel" />
    /// <seealso cref="ShoppingBasket.Service.Models.Discounts.Contracts.IDiscount" />
    public abstract class Discount : BaseModel, IDiscount  //TODO maybe change this, there is a dillema should I leave everything in one model, or as now is, separate ones
    {
        /// <summary>
        /// Gets or sets the type of the discount.
        /// </summary>
        /// <value>
        /// The type of the discount.
        /// </value>
        public DiscountType DiscountType { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the main product identifier.
        /// </summary>
        /// <value>
        /// The main product identifier.
        /// </value>
        public Guid MainProductId { get; set; }

        /// <summary>
        /// Gets or sets priority.
        /// </summary>
        /// <value>
        /// My property.
        /// </value>
        public int Priority { get; set; }
    }
}