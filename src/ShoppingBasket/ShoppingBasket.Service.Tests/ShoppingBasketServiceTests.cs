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
        [InlineData(1, 1, 1, 2.95)] // basket has 1 bread, 1 butter, 1 milk, should return total of 2.95 - no discounts
        [InlineData(2, 0, 2, 3.10)] // basket has 2 breads, 2 butters,  should return total of 2.95 - 2 butters, get 1 bread at 50%
        [InlineData(0, 4, 0, 3.45)] // basket has 4 milks, should return total of 3.45 - buy 3 milks, get 4th milk for free
        [InlineData(1, 8, 2, 9.00)] // basket has 2 butters, 1 bread, 8 milk, should return total of 9.00 - buy 2 butters, get 1 bread at 50%
        public async Task CalculateBasketTotalAsync_ShouldReturnExpectedTotalWithDiscounts(int breadQuantity, int milkQuantity, int butterQuantity, decimal expected)
        {
            //Arrange - arrange values and setup everything
            var basket = await GetShoppingBasketWithItemsAsync(breadQuantity, milkQuantity, butterQuantity);

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

        // extra bread for free quantity is priority discount - for 3 breads, get 2 breads for free
        [Theory]
        [InlineData(5, 0, 2, 4.60)] // 2 breads free
        [InlineData(4, 0, 2, 4.60)] // 1 bread free
        public async Task CalculateBasketTotalAsync_ShouldCalculateQuantityPriorityDiscount(int breadQuantity, int milkQuantity, int butterQuantity, decimal expected)
        {
            //Arrange - arrange values and setup everything
            var basket = await GetShoppingBasketWithItemsAsync(breadQuantity, milkQuantity, butterQuantity);

            basket.Discounts = new List<IDiscount>
            {
                ShoppingBasketFixture.CreateProductPercentageDiscount(50, 3, 3, new DateTime(2022, 8, 25), priority: 2),
                ShoppingBasketFixture.CreateExtraQuantityProductDiscount(3, 1, new DateTime(2022, 8, 25), priority: 1),
                ShoppingBasketFixture.CreateExtraQuantityProductDiscount(3, 2, new DateTime(2022, 8, 25), ShoppingBasketFixture.BreadProduct.id, priority: 1)
            };

            //Act
            await _shoppingBasketService.CalculateBasketTotalAsync(basket);

            //Asert
            Assert.Equal(expected, basket.Total);
        }

        // percentage discount is priority for bread and butter - for 3 butters, get 2 breads at 50%
        [Theory]
        [InlineData(5, 0, 3, 6.40)] // 2 breads at 50%
        [InlineData(1, 0, 6, 5.30)] // 1 bread at 50%
        [InlineData(1, 0, 3, 2.90)] // 1 bread at 50%
        [InlineData(0, 0, 3, 2.40)] // no discounts
        public async Task CalculateBasketTotalAsync_ShouldCalculatePercentagePriorityDiscount(int breadQuantity, int milkQuantity, int butterQuantity, decimal expected)
        {
            //Arrange - arrange values and setup everything
            var basket = await GetShoppingBasketWithItemsAsync(breadQuantity, milkQuantity, butterQuantity);

            basket.Discounts = new List<IDiscount>
            {
                ShoppingBasketFixture.CreateProductPercentageDiscount(50, 3, 2, new DateTime(2022, 8, 25), priority: 1),
                ShoppingBasketFixture.CreateExtraQuantityProductDiscount(3, 1, new DateTime(2022, 8, 25), priority: 2),
                ShoppingBasketFixture.CreateExtraQuantityProductDiscount(3, 2, new DateTime(2022, 8, 25), ShoppingBasketFixture.BreadProduct.id, priority: 2)
            };

            //Act
            await _shoppingBasketService.CalculateBasketTotalAsync(basket);

            //Asert
            Assert.Equal(expected, basket.Total);
        }

        private async Task<IShoppingBasket> GetShoppingBasketWithItemsAsync(int breadQuantity, int milkQuantity, int butterQuantity)
        {
            var basket = await _shoppingBasketService.CreateShoppingBasketAsync("user_cookie_1");
            var products = ShoppingBasketFixture.CreateProducts();

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

            return basket;
        }
    }
}