using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models
{
    public class ShoppingBasket : BaseModel, IShoppingBasket
    {
        public IEnumerable<IShoppingBasketItem> ShoppingBasketItems { get; set; }

        /// <summary>
        /// User identifier that groups shopping basket by unlogged user
        /// </summary>
        public string UserIdentifier { get; set; }

        public IEnumerable<IDiscount> Discounts { get; set; }

        public decimal Total { get; set; }
    }
}