using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Discounts.Enums;

namespace ShoppingBasket.Service.Providers
{
    public class ExtraProductFreeDiscountProvider : IDiscountProvider
    {
        public DiscountType DiscountType => DiscountType.ExtraProductFree;

        public Task<decimal> GetDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            return CalculateDiscountAsync(shoppingBasketItems, discounts.Cast<IProductExtraQuantityDiscount>());
        }

        protected internal Task<decimal> CalculateDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IProductExtraQuantityDiscount> discounts)
        {
            decimal discountResult = 0m;

            foreach (var discount in discounts)
            {
                var mainProduct = shoppingBasketItems.FirstOrDefault(p => p.ProductId == discount.MainProductId);
                if (mainProduct is not null && mainProduct.Quantity >= discount.MainQuantity)
                {
                    var quantityDiscount = (int)Math.Floor(mainProduct.Quantity / (discount.MainQuantity + discount.DiscountQuantity));

                    if (mainProduct.Quantity <= quantityDiscount)
                    {
                        discountResult += mainProduct.Quantity * mainProduct.Product.Price;
                    }

                    if (mainProduct.Quantity > quantityDiscount)
                    {
                        discountResult += quantityDiscount * mainProduct.Product.Price;
                    }
                }
            }

            return Task.FromResult(discountResult);
        }
    }
}