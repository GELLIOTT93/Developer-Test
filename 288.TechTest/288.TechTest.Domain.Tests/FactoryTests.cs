using _288.TechTest.Domain.Factories;
using _288.TechTest.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace _288.TechTest.Domain.Tests
{
    public class FactoryTests
    {
        [Test]
        public void Discount_Factories_Not_In_Active_Date_Returns_Null()
        {
            var basketItem = new BasketItemModel
            {
                BasketId = 1,
                Price = 100,
                ProductCode = "Test",
                Quantity = 1
            };

            var basket = new BasketModel
            {
                BasketItems = new List<BasketItemModel> { basketItem },
                CompanyIdentifier = "Test",
                UserIdentifier = "Test"
            };

            var discount = new DiscountModel
            {
                ActiveFrom = DateTime.Now.AddDays(-2),
                ActiveTo = DateTime.Now.AddDays(-1),
                Amount = 10,
                Code = "Test",
                DiscountType = DiscountTypeEnum.Percent,
                MinimumSpend = 10
            };
            var percentFactory = new PercentDiscountFactory(basket, discount);
            var giftVoucherFactory = new GiftVoucherDiscountFactory(basket, discount);

            var percentBasket = percentFactory.CalculateDiscount();
            var giftVoucherBasket = giftVoucherFactory.CalculateDiscount();

            Assert.IsNull(percentBasket);
            Assert.IsNull(giftVoucherBasket);
        }

        [Test]
        public void Discount_Factories_Below_Minimum_Spend_Returns_Null()
        {
            var basketItem = new BasketItemModel
            {
                BasketId = 1,
                Price = 100,
                ProductCode = "Test",
                Quantity = 1
            };

            var basket = new BasketModel
            {
                BasketItems = new List<BasketItemModel> { basketItem },
                CompanyIdentifier = "Test",
                UserIdentifier = "Test"
            };

            var discount = new DiscountModel
            {
                ActiveFrom = DateTime.Now.AddDays(-2),
                ActiveTo = DateTime.Now.AddDays(-1),
                Amount = 10,
                Code = "Test",
                DiscountType = DiscountTypeEnum.Percent,
                MinimumSpend = 1000
            };
            var percentFactory = new PercentDiscountFactory(basket, discount);
            var giftVoucherFactory = new GiftVoucherDiscountFactory(basket, discount);

            var percentBasket = percentFactory.CalculateDiscount();
            var giftVoucherBasket = giftVoucherFactory.CalculateDiscount();

            Assert.IsNull(percentBasket);
            Assert.IsNull(giftVoucherBasket);
        }

        [Test]
        public void Percent_Discount_Factory_Processes_Discount_Correct()
        {
            var basketItem = new BasketItemModel
            {
                BasketId = 1,
                Price = 100,
                ProductCode = "Test",
                Quantity = 1
            };

            var basket = new BasketModel
            {
                BasketItems = new List<BasketItemModel> { basketItem },
                CompanyIdentifier = "Test",
                UserIdentifier = "Test"
            };

            var discount = new DiscountModel
            {
                ActiveFrom = DateTime.Now.AddDays(-1),
                ActiveTo = DateTime.Now.AddDays(1),
                Amount = 10,
                Code = "Test",
                DiscountType = DiscountTypeEnum.Percent,
                MinimumSpend = 10
            };

            var percentFactory = new PercentDiscountFactory(basket, discount);
            var giftVoucherFactory = new GiftVoucherDiscountFactory(basket, discount);
            var noDiscountFactory = new NoDiscountFactory(basket, discount);

            var percentBasket = percentFactory.CalculateDiscount();
            var giftVoucherBasket = giftVoucherFactory.CalculateDiscount();
            var noDiscountBasket = noDiscountFactory.CalculateDiscount();

            Assert.AreEqual(percentBasket.Total, 100);
            Assert.AreEqual(percentBasket.TotalWithDiscount, 90);
            Assert.AreEqual(giftVoucherBasket.Total, 100);
            Assert.AreEqual(giftVoucherBasket.TotalWithDiscount, 90);
            Assert.AreEqual(noDiscountBasket.Total, 100);
            Assert.AreEqual(noDiscountBasket.TotalWithDiscount, 100);
        }
    }
}
