using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Discounts.Enums;

namespace ShoppingBasket.Service.Models.Discounts
{
    public class Discount : BaseModel, IDiscount
    {
        public DiscountType DiscountType { get; set; }

        public DateTime EndDate { get; set; }

        public bool ExcludeOtherDiscounts { get; set; }

        public DateTime StartDate { get; set; }

        public Guid MainProductId { get; set; }
    }
}