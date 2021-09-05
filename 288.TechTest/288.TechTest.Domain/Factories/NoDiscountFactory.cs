using _288.TechTest.Domain.Models;

namespace _288.TechTest.Domain.Factories
{
    public class NoDiscountFactory : BasketDiscountFactory
    {
        private readonly BasketModel basket;
        private readonly DiscountModel discount;

        public NoDiscountFactory(BasketModel basket, DiscountModel discount) : base(basket, discount)
        {
            this.basket = basket;
            this.discount = discount;
        }
        public override ProcessedBasketModel CalculateDiscount()
        {
            var processedBasket = basket.MapToProcessed(basket);
            processedBasket.TotalWithDiscount = basket.Total;

            return processedBasket;
        }
    }
}
