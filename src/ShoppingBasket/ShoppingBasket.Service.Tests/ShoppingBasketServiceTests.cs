using ShoppingBasket.Service.Infrastructure.Factories;
using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;
using ShoppingBasket.Service.Providers;
using ShoppingBasket.Service.Services.ShoppingBasketDetails;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ShoppingBasket.Service.Tests
{
    [ExcludeFromCodeCoverage]
    public class ShoppingBasketServiceTests
    {
        private readonly IEnumerable<IDiscountProvider> _providers;
        private readonly ShoppingBasketService _shoppingBasketService;

        public ShoppingBasketServiceTests()
        {
            _providers = new List<IDiscountProvider>
            {
                new AnotherProductPercentageDiscountProvider(),
                new ProductQuantityDiscountProvider()
            };
            _shoppingBasketService = new ShoppingBasketService(new BasketCalculationService(_providers.ToList()), LoggerFactory.CreateShoppingLogger());
        }

        [Theory]
        [InlineData(1, 1, 1, 2.95)] // basket has 1 bread, 1 butter, 1 milk, should return total of 2.95
        [InlineData(2, 0, 2, 3.10)] // basket has 2 breads, 2 butters,  should return total of 2.95
        [InlineData(0, 4, 0, 3.45)] // basket has 4 milks, should return total of 3.45
        [InlineData(1, 8, 2, 9.00)] // basket has 2 butters, 1 bread, 8 milk, should return total of 9.00
        public async Task CalculateBasketTotalAsync_ShouldReturnExpectedTotalWithDiscounts(int breadQuantity, int milkQuantity, int butterQuantity, decimal expected)
        {
            //Arrange - arrange values and setup everything

            var products = ShoppingBasketFixture.CreateProducts();

            var basket = await _shoppingBasketService.CreateShoppingBasketAsync("user_cookie_1");

            var newShoppingBasketItems = new List<IShoppingBasketItem>();

            foreach (var product in products)
            {
                IShoppingBasketItem item = new ShoppingBasketFixture.ShoppingBasketItemMock { ShoppingBasketId = basket.Id };

                if (product.Id.Equals(ShoppingBasketFixture.ButterProduct.id) && butterQuantity > 0)
                {
                    item.Quantity = butterQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                if (product.Id.Equals(ShoppingBasketFixture.BreadProduct.id) && breadQuantity > 0)
                {
                    item.Quantity = breadQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                if (product.Id.Equals(ShoppingBasketFixture.MilkProduct.id) && milkQuantity > 0)
                {
                    item.Quantity = milkQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }
            }

            basket.ShoppingBasketItems = newShoppingBasketItems;
            basket.Discounts = new List<IDiscount>
            {
                ShoppingBasketFixture.CreateProductPercentageDiscount(50, 2, 1, new DateTime(2022, 8, 25)),
                ShoppingBasketFixture.CreateExtraQuantityProductDiscount(3, 1, new DateTime(2022, 8, 25))
            };

            //Act
            await _shoppingBasketService.CalculateBasketTotalAsync(basket);

            //Asert
            Assert.Equal(expected, basket.Total);
        }

        [Theory]
        [InlineData(5, 0, 2, 4.60)] // extra bread quantity is priority discount
        public async Task CalculateBasketTotalAsync_ShouldCalculatePriorityDiscount(int breadQuantity, int milkQuantity, int butterQuantity, decimal expected)
        {
            //Arrange - arrange values and setup everything

            var products = ShoppingBasketFixture.CreateProducts();

            var basket = await _shoppingBasketService.CreateShoppingBasketAsync("user_cookie_1");

            var newShoppingBasketItems = new List<IShoppingBasketItem>();

            foreach (var product in products)
            {
                IShoppingBasketItem item = new ShoppingBasketFixture.ShoppingBasketItemMock { ShoppingBasketId = basket.Id };

                if (product.Id.Equals(ShoppingBasketFixture.ButterProduct.id) && butterQuantity > 0)
                {
                    item.Quantity = butterQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                if (product.Id.Equals(ShoppingBasketFixture.BreadProduct.id) && breadQuantity > 0)
                {
                    item.Quantity = breadQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                if (product.Id.Equals(ShoppingBasketFixture.MilkProduct.id) && milkQuantity > 0)
                {
                    item.Quantity = milkQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }
            }

            basket.ShoppingBasketItems = newShoppingBasketItems;
            basket.Discounts = new List<IDiscount>
            {
                ShoppingBasketFixture.CreateProductPercentageDiscount(50, 3, 1, new DateTime(2022, 8, 25)),
                ShoppingBasketFixture.CreateExtraQuantityProductDiscount(3, 1, new DateTime(2022, 8, 25)),
                ShoppingBasketFixture.CreateExtraQuantityProductDiscount(3, 2, new DateTime(2022, 8, 25), ShoppingBasketFixture.BreadProduct.id)
            };

            //Act
            await _shoppingBasketService.CalculateBasketTotalAsync(basket);

            //Asert
            Assert.Equal(expected, basket.Total);
        }
    }
}