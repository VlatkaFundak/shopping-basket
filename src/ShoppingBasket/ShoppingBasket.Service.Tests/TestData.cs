using ShoppingBasket.Service.Models.Discounts;
using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Discounts.Enums;
using ShoppingBasket.Service.Models.Discounts.Factories;
using ShoppingBasket.Service.Models.Products;
using ShoppingBasket.Service.Models.Products.Contracts;

namespace ShoppingBasket.Service.Tests
{
    public static class TestData
    {
        private static IDiscountFactory _discountFactory;

        static TestData()
        {
            _discountFactory = DiscountFacade.CreateDiscountFactory();
        }

        public static IDictionary<string, Guid> ProductCategoryDictionary = new Dictionary<string, Guid>
        {
            { "Baked goods", Guid.Parse("ce450579-3998-4140-b698-0a2843912140") },
            { "Dairy goods", Guid.Parse("5802b263-f01e-4f48-ac66-3c14f34d208a") }
        };

        public static IEnumerable<IProduct> CreateProducts()
        {
            IList<IProduct> products = new List<IProduct>
            {
                CreateProduct(Guid.Parse("6e79a5b7-e196-4f73-8767-628552fbd26f"), ProductCategoryDictionary["Baked goods"], 1.00m, "Bread"),
                CreateProduct(Guid.Parse("5d61b761-9d0d-4194-bdce-7d877a192625"), ProductCategoryDictionary["Dairy goods"], 1.15m, "Milk"),
                CreateProduct(Guid.Parse("07e0f7b4-7904-4bef-8a09-a2c721b2bbe3"), ProductCategoryDictionary["Dairy goods"], 0.80m, "Butter")
            };

            return products;
        }

        public static IProduct CreateProduct(Guid id, Guid productCategoryId, decimal price, string name)
        {
            var product = new Product();
            product.ProductCategoryId = productCategoryId;
            product.Id = id;
            product.DateCreated = DateTime.UtcNow;
            product.DateUpdated = DateTime.UtcNow;
            product.Price = price;
            product.Name = name;

            return product;
        }

        // buy x get y percentage discount
        internal static IAnotherProductPercentageDiscount CreatProductPercentageDiscount(int percentage, int quantity, int discountQuantity, DateTime startDate,
            bool excludeOtherDiscounts = false, Guid? mainProductId = null, Guid? discountProductId = null)
        {
            var discount = new AnotherProductPercentageDiscount
            {
                DiscountType = DiscountType.ProductPercentage,
                MainProductId = mainProductId ?? Guid.Parse("07e0f7b4-7904-4bef-8a09-a2c721b2bbe3"),
                DiscountProductId = discountProductId ?? Guid.Parse("6e79a5b7-e196-4f73-8767-628552fbd26f"),
                MainQuantity = quantity,
                DiscountQuantity = discountQuantity,
                Percentage = percentage,
                StartDate = startDate,
                EndDate = startDate.AddDays(15),
                ExcludeOtherDiscounts = excludeOtherDiscounts
            };

            return discount;
        }

        // buy x, get x/ free
        internal static IProductExtraQuantityDiscount CreateExtraQuantityProductDiscount(int quantity, int discountQuantity, DateTime startDate,
            bool excludeOtherDiscounts = false, Guid? mainProductId = null)
        {
            var discount = new ProductExtraQuantityDiscount
            {
                DiscountType = DiscountType.ExtraProductFree,
                MainProductId = mainProductId ?? Guid.Parse("5d61b761-9d0d-4194-bdce-7d877a192625"),
                MainQuantity = quantity,
                DiscountQuantity = discountQuantity,
                StartDate = startDate,
                EndDate = startDate.AddDays(20),
                ExcludeOtherDiscounts = excludeOtherDiscounts
            };

            return discount;
        }

        public static IEnumerable<IDiscount> GetDiscounts()
        {
            IList<IDiscount> discounts = new List<IDiscount>
            {
                CreatProductPercentageDiscount(50, 2, 1, new DateTime(2022, 8, 25)),
                CreateExtraQuantityProductDiscount(3, 1, new DateTime(2022, 8, 25))
            };

            return discounts;
        }
    }
}