using Moq;
using ShoppingBasket.Service.Common.Filters;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;
using ShoppingBasket.Service.Services.ShoppingBasketDetails;
using ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ShoppingBasket.Service.Tests
{
    [ExcludeFromCodeCoverage]
    public class ShoppingBasketItemServiceTests
    {
        private readonly Fixture _fixture;

        public ShoppingBasketItemServiceTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async void AddShoppingBasketItemAsync_ShouldAddNewItems()
        {
            //Arange
            var products = TestData.CreateProducts();
            var shoppingBasketItemService = new ShoppingBasketItemService();

            var basket = await _fixture.shoppingBasketService.Object.CreateShoppingBasketAsync("user_cookie_info_1");

            var items = await shoppingBasketItemService.FindShoppingBasketItemAsync(_fixture.shoppingBasketItemFilterParams.Object, null, null);

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
            items = await shoppingBasketItemService.FindShoppingBasketItemAsync(_fixture.shoppingBasketItemFilterParams.Object, null, null);

            //Assert
            Assert.Equal(expected, items.Count());
        }

        private class Fixture
        {
            public Mock<IBasketCalculationService> basketCalculationService;
            public Mock<IShoppingBasketService> shoppingBasketService;
            public Mock<IShoppingBasketItemFilterParams> shoppingBasketItemFilterParams;

            public Fixture()
            {
                basketCalculationService = new Mock<IBasketCalculationService>();
                shoppingBasketService = new Mock<IShoppingBasketService>();
                shoppingBasketItemFilterParams = new Mock<IShoppingBasketItemFilterParams>();

                IShoppingBasket shoppingBasket = new TestData.ShoppingBasketMock
                {
                    Id = Guid.NewGuid(),
                    UserIdentifier = "user_cookie_info_1",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                };

                shoppingBasketItemFilterParams.Setup(p => p.ShoppingBasketId).Returns(shoppingBasket.Id);

                shoppingBasketService.Setup(p => p.CreateShoppingBasketAsync(shoppingBasket.UserIdentifier))
                    .Returns(Task.FromResult(shoppingBasket));
            }
        }
    }
}