using ShoppingBasket.Service.Models.Discounts.Enums;

namespace ShoppingBasket.Service.Models.Discounts.Contracts
{
    public interface IDiscount : IBaseModel
    {
        DiscountType DiscountType { get; set; }

        DateTime EndDate { get; set; }

        bool ExcludeOtherDiscounts { get; set; }

        DateTime StartDate { get; set; }

        Guid MainProductId { get; set; }
    }
}