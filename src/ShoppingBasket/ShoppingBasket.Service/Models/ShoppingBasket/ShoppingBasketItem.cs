using ShoppingBasket.Service.Models;
using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service
{
    public class ShoppingBasketItem : BaseModel, IShoppingBasketItem
    {
        public Guid ShoppingBasketId { get; set; }

        public decimal Quantity { get; set; }

        public Guid ProductId { get; set; }

        public IProduct Product { get; set; }
    }
}