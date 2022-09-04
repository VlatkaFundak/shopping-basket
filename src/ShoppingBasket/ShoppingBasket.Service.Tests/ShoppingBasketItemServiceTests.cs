using Moq;
using ShoppingBasket.Service.Common.Filters;
using ShoppingBasket.Service.Services.Shopping;
using Xunit;

namespace ShoppingBasket.Service.Tests
{
    public class ShoppingBasketItemServiceTests
    {
        [Fact]
        public async void AddShoppingBasketItemAsync_ShouldAddNewItems()
        {
            //Arange
            var products = TestData.CreateProducts();
            var shoppingBasketItemService = new ShoppingBasketItemService();
            var shoppingBasketService = new ShoppingBasketService(Mock.Of<IBasketCalculationService>());

            var basket = await shoppingBasketService.CreateShoppingBasketAsync("user_cookie_1");

            var items = await shoppingBasketItemService.FindShoppingBasketItemAsync(new ShoppingBasketItemFilterParams { ShoppingBasketId = basket.Id }, null, null);
            var expected = items.Count() + products.Count();
            var newShoppingBasketItems = new List<IShoppingBasketItem>();

            await Task.WhenAll(products.Select(async (product) =>
            {
                var item = await shoppingBasketItemService.CreateShoppingBasketItemAsync(basket.Id);
                item.ProductId = product.Id;
                item.Product = product;
                item.Quantity = 5;

                newShoppingBasketItems.Add(item);
            }).ToArray());

            //Act
            await shoppingBasketItemService.AddShoppingBasketItemAsync(newShoppingBasketItems);
            items = await shoppingBasketItemService.FindShoppingBasketItemAsync(new ShoppingBasketItemFilterParams { ShoppingBasketId = basket.Id }, null, null);

            //Assert
            Assert.Equal(expected, items.Count());
        }
    }
}