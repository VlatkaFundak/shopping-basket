namespace ShoppingBasket.Service.Models.Discounts.Contracts
{
    public interface IProductExtraQuantityDiscount : IDiscount
    {
        int DiscountQuantity { get; set; }
        int MainQuantity { get; set; }
    }
}