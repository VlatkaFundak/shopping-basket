namespace ShoppingBasket.Service.Models.Discounts.Contracts
{
    public interface IAnotherProductPercentageDiscount : IDiscount
    {
        Guid DiscountProductId { get; set; }
        int DiscountQuantity { get; set; }
        int MainQuantity { get; set; }
        int Percentage { get; set; }
    }
}