using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service.Models.Products
{
    public class ProductCategory : BaseModel, IProductCategory
    {
        public string Name { get; set; }

        public string Abrv { get; set; }

        public string Description { get; set; }
    }
}