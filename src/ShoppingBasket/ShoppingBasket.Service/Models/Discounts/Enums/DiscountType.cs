namespace ShoppingBasket.Service.Models.Discounts.Enums
{
    [Flags]
    public enum DiscountType
    {
        None = 1,
        ProductPercentage = 2,
        ExtraProductFree = 4
    }
}