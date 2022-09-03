using ShoppingBasket.Service.Models;
using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service
{
    public interface IShoppingBasketItem : IBaseModel
    {
        Guid ShoppingBasketId { get; set; }

        decimal Quantity { get; set; }

        Guid ProductId { get; set; }

        IProduct Product { get; set; }
    }
}