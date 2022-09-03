using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service.Models.Products
{
    public class Product : BaseModel, IProduct
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Guid ProductCategoryId { get; set; }

        public IEnumerable<IDiscount> Discounts { get; set; }
    }
}