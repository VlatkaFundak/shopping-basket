using ShoppingBasket.Service.Common.Enums;
using ShoppingBasket.Service.Models.Discounts;
using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Products;
using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service.Tests
{
    internal static class ShoppingBasketFixture
    {
        public static (string categoryName, Guid id) ProductBakedGoodsCategoryName = new("Baked goods", Guid.Parse("ce450579-3998-4140-b698-0a2843912140"));
        public static (string categoryName, Guid id) ProductDairyGoodsCategoryName = new("Dairy goods", Guid.Parse("5802b263-f01e-4f48-ac66-3c14f34d208a"));

        public static (string productName, Guid id, decimal price) BreadProduct = new("Bread", Guid.Parse("6e79a5b7-e196-4f73-8767-628552fbd26f"), 1.00m);
        public static (string productName, Guid id, decimal price) MilkProduct = new("Milk", Guid.Parse("5d61b761-9d0d-4194-bdce-7d877a192625"), 1.15m);
        public static (string productName, Guid id, decimal price) ButterProduct = new("Butter", Guid.Parse("07e0f7b4-7904-4bef-8a09-a2c721b2bbe3"), 0.80m);

        public static IEnumerable<IProduct> CreateProducts()
        {
            IList<IProduct> products = new List<IProduct>
            {
                CreateProduct(BreadProduct.id, ProductBakedGoodsCategoryName.id, BreadProduct.price, BreadProduct.productName),
                CreateProduct(MilkProduct.id, ProductDairyGoodsCategoryName.id, MilkProduct.price, MilkProduct.productName),
                CreateProduct(ButterProduct.id, ProductDairyGoodsCategoryName.id, ButterProduct.price, ButterProduct.productName)
            };

            return products;
        }

        public static IProduct CreateProduct(Guid id, Guid productCategoryId, decimal price, string name)
        {
            var product = new Product
            {
                ProductCategoryId = productCategoryId,
                Id = id,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Price = price,
                Name = name
            };

            return product;
        }

        // buy x get y percentage discount
        public static IAnotherProductPercentageDiscount CreateProductPercentageDiscount(int percentage, int quantity, int discountQuantity, DateTime startDate,
            Guid? mainProductId = null, Guid? discountProductId = null, int priority = 2)
        {
            var discount = new AnotherProductPercentageDiscount
            {
                DiscountType = DiscountType.AnotherProductPercentage,
                MainProductId = mainProductId ?? ButterProduct.id,
                DiscountProductId = discountProductId ?? BreadProduct.id,
                MainQuantity = quantity,
                DiscountQuantity = discountQuantity,
                Percentage = percentage,
                StartDate = startDate,
                EndDate = startDate.AddDays(15),
                Priority = priority
            };

            return discount;
        }

        // buy x, get x free
        public static IProductQuantityDiscount CreateExtraQuantityProductDiscount(int quantity, int discountQuantity, DateTime startDate,
            Guid? mainProductId = null, int priority = 1)
        {
            var discount = new ProductQuantityDiscount
            {
                DiscountType = DiscountType.ProductQuantity,
                MainProductId = mainProductId ?? MilkProduct.id,
                MainQuantity = quantity,
                DiscountQuantity = discountQuantity,
                StartDate = startDate,
                EndDate = startDate.AddDays(20),
                Priority = priority
            };

            return discount;
        }

        public static IEnumerable<IDiscount> FindActiveDiscounts()
        {
            IList<IDiscount> discounts = new List<IDiscount>
            {
                CreateProductPercentageDiscount(50, 2, 1, new DateTime(2022, 8, 25)),
                CreateExtraQuantityProductDiscount(3, 1, new DateTime(2022, 8, 25))
            };

            return discounts;
        }
    }
}