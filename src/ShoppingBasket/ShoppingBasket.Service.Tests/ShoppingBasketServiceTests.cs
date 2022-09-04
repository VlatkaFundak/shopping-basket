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
        [Theory]
        [InlineData(1, 1, 1, 2.95)]
        [InlineData(2, 0, 2, 3.10)]
        [InlineData(0, 4, 0, 3.45)]
        [InlineData(1, 8, 2, 9.00)]
        public async Task CalculateBasketTotalAsync_ShouldReturnTrue(int breadQuantity, int milkQuantity, int butterQuantity, decimal expected)
        {
            //Arrange - arrange values and setup everything

            var products = TestData.CreateProducts();
            IEnumerable<IDiscountProvider> providers = new List<IDiscountProvider>
            {
                new AnotherProductPercentageDiscountProvider(),
                new ProductQuantityDiscountProvider()
            };

            var shoppingBasketService = new ShoppingBasketService(new BasketCalculationService(providers.ToList()));

            var basket = await shoppingBasketService.CreateShoppingBasketAsync("user_cookie_1");

            var newShoppingBasketItems = new List<IShoppingBasketItem>();

            _ = products.Select(product =>
            {
                IShoppingBasketItem item = new TestData.ShoppingBasketItemMock { ShoppingBasketId = basket.Id };

                if (product.Id.Equals(TestData.ButterProduct.id) && butterQuantity > 0)
                {
                    item.Quantity = butterQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                if (product.Id.Equals(TestData.BreadProduct.id) && breadQuantity > 0)
                {
                    item.Quantity = breadQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                if (product.Id.Equals(TestData.MilkProduct.id) && milkQuantity > 0)
                {
                    item.Quantity = milkQuantity;
                    item.ProductId = product.Id;
                    item.Product = product;
                    newShoppingBasketItems.Add(item);
                }

                return true;
            }).ToArray();

            basket.ShoppingBasketItems = newShoppingBasketItems;
            basket.Discounts = TestData.FindActiveDiscounts();

            //Act
            await shoppingBasketService.CalculateBasketTotalAsync(basket);

            //Asert
            Assert.Equal(expected, basket.Total);
        }

        [Fact(Skip = "Do not run now")]
        public void CalculateBasketTotalAsync_EmptyItemsShouldThrowException()
        {
            //Arrange

            //Act

            //Asert
        }
    }
}