using _288.TechTest.Domain.Models;
using System;
using System.Linq;

namespace _288.TechTest.Domain.Factories
{
    public abstract class BasketDiscountFactory
    {
        private readonly BasketModel basket;
        private readonly DiscountModel discount;

        protected BasketDiscountFactory(BasketModel basket, DiscountModel discount)
        {
            this.basket = basket;
            this.discount = discount;
        }

        public abstract ProcessedBasketModel CalculateDiscount();

        public bool ValidateVoucher()
        {
            if (discount.ActiveTo < DateTime.Now || discount.ActiveFrom > DateTime.Now)
                return false;

            var basketValue = basket.BasketItems.Sum(x => x.Price * x.Quantity);

            if (basketValue <= discount.MinimumSpend)
                return false;

            return true;
        }
    }
}
