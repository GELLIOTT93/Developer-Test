using _288.TechTest.Domain.Models;

namespace _288.TechTest.Domain.Factories
{
    public class PercentDiscountFactory : BasketDiscountFactory
    {
        private readonly BasketModel basket;
        private readonly DiscountModel discount;

        public PercentDiscountFactory(BasketModel basket, DiscountModel discount) : base(basket, discount)
        {
            this.basket = basket;
            this.discount = discount;
        }
        public override ProcessedBasketModel CalculateDiscount()
        {
            if (!ValidateVoucher())
                return null;

            if (discount.Amount > 100)
                return null;

            var processedBasket = basket.MapToProcessed(basket);
            processedBasket.TotalWithDiscount = (basket.Total / 100) * (100M - discount.Amount);

            return processedBasket;
        }
    }
}
