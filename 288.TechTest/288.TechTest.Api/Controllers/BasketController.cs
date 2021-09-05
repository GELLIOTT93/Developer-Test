using _288.TechTest.Domain.Factories;
using _288.TechTest.Domain.Interfaces;
using _288.TechTest.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _288.TechTest.Api.Controllers
{
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService basketService;
        private readonly IDiscountService discountService;

        public BasketController(IBasketService basketService, IDiscountService discountService)
        {
            this.basketService = basketService;
            this.discountService = discountService;
        }

        [HttpPost("/basket/get")]
        public async Task<IActionResult> GetBasketForUser(GetBasketModel model)
        {
            if (model == null)
                return BadRequest($"get {nameof(model)} must not be null");

            var basket = await basketService.GetBasketForUser(model.UserIdentifier, model.CompanyIdentifier);

            if (basket == null)
                return NotFound("Basket not found");

            var factory = (BasketDiscountFactory)null;

            var discount = new DiscountModel();

            // Do not like this
            if (string.IsNullOrWhiteSpace(model.DiscountCode))
            {
                discount.DiscountType = DiscountTypeEnum.NoDiscount;
            }
            else
            {
                discount = await discountService.GetDiscountByCodeAndCompanyId(model.DiscountCode, model.CompanyIdentifier);
            }

            // Do not like this
            if (discount == null)
            {
                discount = new DiscountModel();
                discount.DiscountType = DiscountTypeEnum.NoDiscount;
            }

            // Factory
            switch (discount.DiscountType)
            {
                case DiscountTypeEnum.GiftVoucher:
                    factory = new GiftVoucherDiscountFactory(basket, discount);
                    break;
                case DiscountTypeEnum.Percent:
                    factory = new PercentDiscountFactory(basket, discount);
                    break;
                case DiscountTypeEnum.NoDiscount:
                    factory = new NoDiscountFactory(basket, discount);
                    break;
                default:
                    break;
            }

            if(factory == null)
                return BadRequest("Voucher not compatible");

            var processedBasket = factory.CalculateDiscount();

            return Ok(processedBasket);
        }

        [HttpPut("/basket/update")]
        public async Task<IActionResult> UpdateBasketForUser([FromBody] UpdateBasketItemModel model)
        {
            if (model == null)
                return BadRequest($"update {nameof(model)} must not be null");

            var basket = await basketService.UpdateBasket(model);
            return Ok(basket);
        }

        [HttpPut("/basket/update/multiple")]
        public async Task<IActionResult> UpdateMultipleBasketForUser([FromBody] UpdateMultipleBasketItemModel model)
        {
            if (model == null)
                return BadRequest($"update multiple {nameof(model)} must not be null");

            var basket = await basketService.UpdateMultipleBasketItem(model);
            return Ok(basket);
        }
    }
}
