using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models
{
    public interface IShoppingBasket : IBaseModel
    {
        /// <summary>
        /// User identifier that groups shopping basket by unlogged user
        /// </summary>
        string UserIdentifier { get; set; }

        IEnumerable<IDiscount> Discounts { get; set; }

        IEnumerable<IShoppingBasketItem> ShoppingBasketItems { get; set; }

        decimal Total { get; internal set; }
    }
}