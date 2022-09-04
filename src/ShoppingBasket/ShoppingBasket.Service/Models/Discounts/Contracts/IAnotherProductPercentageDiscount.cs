﻿namespace ShoppingBasket.Service.Models.Discounts.Contracts
{
    /// <summary>
    /// Another product percentage discount contract
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Models.Discounts.Contracts.IDiscount" />
    public interface IAnotherProductPercentageDiscount : IDiscount
    {
        /// <summary>
        /// Gets or sets the discount product identifier.
        /// </summary>
        /// <value>
        /// The discount product identifier.
        /// </value>
        Guid DiscountProductId { get; set; }

        /// <summary>
        /// Gets or sets the discount quantity.
        /// </summary>
        /// <value>
        /// The discount quantity.
        /// </value>
        int DiscountQuantity { get; set; }

        /// <summary>
        /// Gets or sets the main quantity.
        /// </summary>
        /// <value>
        /// The main quantity.
        /// </value>
        int MainQuantity { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        int Percentage { get; set; }
    }
}