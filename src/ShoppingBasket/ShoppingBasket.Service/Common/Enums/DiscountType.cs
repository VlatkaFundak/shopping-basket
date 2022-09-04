namespace ShoppingBasket.Service.Common.Enums
{
    /// <summary>
    /// Avaliable discount type enums
    /// </summary>
    [Flags]
    public enum DiscountType
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 1,

        /// <summary>
        /// The product percentage
        /// </summary>
        AnotherProductPercentage = 2,

        /// <summary>
        /// The product quantity
        /// </summary>
        ProductQuantity = 4
    }
}