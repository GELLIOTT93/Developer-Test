using _288.TechTest.Domain.Models;

namespace _288.TechTest.Domain.Factories
{
    public class GiftVoucherDiscountFactory : BasketDiscountFactory
    {
        private readonly BasketModel basket;
        private readonly DiscountModel discount;

        public GiftVoucherDiscountFactory(BasketModel basket, DiscountModel discount) : base(basket, discount)
        {
            this.basket = basket;
            this.discount = discount;
        }

        public override ProcessedBasketModel CalculateDiscount()
        {
            if (!ValidateVoucher())
                return null;

            var processedBasket = basket.MapToProcessed(basket);
            processedBasket.TotalWithDiscount = basket.Total - discount.Amount;

            return processedBasket;
        }
    }
}
