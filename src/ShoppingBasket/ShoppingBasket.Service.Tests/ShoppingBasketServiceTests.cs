using ShoppingBasket.Service.Common.Filters;
using ShoppingBasket.Service.Providers;
using ShoppingBasket.Service.Services.Shopping;
using Xunit;

namespace ShoppingBasket.Service.Tests
{
    public class ShoppingBasketServiceTests
    {
        [Theory]
        [InlineData(1, 1, 1, 2.95)]
        [InlineData(2, 0, 2, 3.10)]
        [InlineData(0, 4, 0, 3.45)]
        [InlineData(1, 8, 2, 9.00)]
        public async Task CalculateBasketTotalAsync_Should_Return_(int breadQuantity, int milkQuantity, int butterQuantity, decimal result)
        {
            //Arrange - arrange values and setup everything
            decimal expected = result;
            var products = TestData.CreateProducts();
            var shoppingBasketItemService = new ShoppingBasketItemService();
            IEnumerable<IDiscountProvider> providers = new List<IDiscountProvider>
            {
                new AnotherProductPercentageDiscountProvider(),
                new ExtraProductFreeDiscountProvider()
            };

            var calculationBasketService = new BasketCalculationService(providers.ToArray());
            var shoppingBasketService = new ShoppingBasketService(calculationBasketService);

            var basket = await shoppingBasketService.CreateShoppingBasketAsync("user_cookie_1");
            var newShoppingBasketItems = new List<IShoppingBasketItem>();

            foreach (var product in products)
            {
                var item = await shoppingBasketItemService.CreateShoppingBasketItemAsync(basket.Id);

                if (product.Id.Equals(Guid.Parse("07e0f7b4-7904-4bef-8a09-a2c721b2bbe3")) && butterQuantity > 0)
                {
                    item.Quantity = butterQuantity;

                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                if (product.Id.Equals(Guid.Parse("6e79a5b7-e196-4f73-8767-628552fbd26f")) && breadQuantity > 0)
                {
                    item.Quantity = breadQuantity;

                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                if (product.Id.Equals(Guid.Parse("5d61b761-9d0d-4194-bdce-7d877a192625")) && milkQuantity > 0)
                {
                    item.Quantity = milkQuantity;

                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }
            }
            await shoppingBasketItemService.AddShoppingBasketItemAsync(newShoppingBasketItems);
            var items = await shoppingBasketItemService.FindShoppingBasketItemAsync(new ShoppingBasketItemFilterParams { UserIdentifier = basket.UserIdentifier });

            basket.ShoppingBasketItems = items;
            basket.Discounts = TestData.GetDiscounts();

            //Act
            await shoppingBasketService.CalculateBasketTotalAsync(basket);

            //Asert

            Assert.Equal(expected, basket.Total);
        }

        [Fact(Skip = "Do not run now")]
        public void CalculateBasketTotalAsync_ShouldThrowException()
        {
            //Arrange

            //Act

            //Asert
        }
    }
}