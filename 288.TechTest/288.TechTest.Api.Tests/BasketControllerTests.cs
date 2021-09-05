using _288.TechTest.Api.Controllers;
using _288.TechTest.Domain.Interfaces;
using _288.TechTest.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _288.TechTest.Api.Tests
{
    public class Tests
    {
        private Mock<IBasketService> basketServiceMock;
        private Mock<IDiscountService> discountServiceMock;
        private BasketController basketController;

        [SetUp]
        public void Setup()
        {
            basketServiceMock = new Mock<IBasketService>(MockBehavior.Strict);
            discountServiceMock = new Mock<IDiscountService>(MockBehavior.Strict);
            basketController = new BasketController(basketServiceMock.Object, discountServiceMock.Object);
        }

        [TearDown]
        public void Down()
        {
            basketServiceMock.VerifyAll();
            discountServiceMock.VerifyAll();
        }

        [Test]
        public async Task Get_Basket_Null_Basket_Returns_404()
        {
            var basketReturn = (BasketModel)null;
            basketServiceMock.Setup(x => x.GetBasketForUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basketReturn);

            var result = await basketController.GetBasketForUser(new GetBasketModel {
                UserIdentifier = "test",
                CompanyIdentifier = "test"
            });

            var typedResult = result as ObjectResult;

            Assert.IsAssignableFrom<NotFoundObjectResult>(result);

            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)typedResult.StatusCode);
            Assert.AreEqual(typedResult.Value, "Basket not found");
        }

        [Test]
        public async Task Get_Basket_Null_Request_Object_Returns_404()
        {
            var result = await basketController.GetBasketForUser(null);

            var typedResult = result as ObjectResult;

            Assert.IsAssignableFrom<BadRequestObjectResult>(result);

            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)typedResult.StatusCode);
            Assert.AreEqual(typedResult.Value, "get model must not be null");
        }

        [Test]
        public async Task Get_Basket_Null_Discount_Does_Not_Reduce_Basket_Total()
        {
            var basketItem = new BasketItemModel
            {
                Price = 100m,
                ProductCode = "test",
                Quantity = 2
            };

            var basketReturn = new BasketModel {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                BasketItems = new List<BasketItemModel> { basketItem }
            };

            var requestModel = new GetBasketModel
            {
                UserIdentifier = "test",
                CompanyIdentifier = "test",
                DiscountCode = null
            };

            basketServiceMock.Setup(x => x.GetBasketForUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basketReturn);

            var result = await basketController.GetBasketForUser(requestModel);

            var typedResult = result as ObjectResult;
            var content = typedResult.Value as ProcessedBasketModel;


            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)typedResult.StatusCode);

            Assert.AreEqual(content.Total, 200);
            Assert.AreEqual(content.TotalWithDiscount, 200);
            Assert.AreEqual(content.BasketItems.Count(), 1);
        }

        [Test]
        public async Task Get_Basket_Percent_Discount_Does_Reduce_Basket_Total()
        {
            var basketItem1 = new BasketItemModel
            {
                Price = 100m,
                ProductCode = "test",
                Quantity = 2
            };

            var basketItem2 = new BasketItemModel
            {
                Price = 50m,
                ProductCode = "test2",
                Quantity = 2
            };

            var basketReturn = new BasketModel
            {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                BasketItems = new List<BasketItemModel> { basketItem1, basketItem2 }
            };

            var discountReturn = new DiscountModel
            {
                ActiveFrom = DateTime.MinValue,
                ActiveTo = DateTime.MaxValue,
                MinimumSpend = 10,
                Amount = 10,
                Code = "DiscountTest",
                DiscountType = DiscountTypeEnum.Percent
            };

            var requestModel = new GetBasketModel
            {
                UserIdentifier = "test",
                CompanyIdentifier = "test",
                DiscountCode = "DiscountTest"
            };

            basketServiceMock.Setup(x => x.GetBasketForUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basketReturn);
            discountServiceMock.Setup(x => x.GetDiscountByCodeAndCompanyId(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(discountReturn);

            var result = await basketController.GetBasketForUser(requestModel);

            var typedResult = result as ObjectResult;
            var content = typedResult.Value as ProcessedBasketModel;


            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)typedResult.StatusCode);

            Assert.AreEqual(content.Total, 300);
            Assert.AreEqual(content.TotalWithDiscount, 270);
            Assert.AreEqual(content.BasketItems.Count(), 2);
        }

        [Test]
        public async Task Get_Basket_Gift_Voucher_Discount_Does_Reduce_Basket_Total()
        {
            var basketItem1 = new BasketItemModel
            {
                Price = 100m,
                ProductCode = "test",
                Quantity = 2
            };

            var basketItem2 = new BasketItemModel
            {
                Price = 50m,
                ProductCode = "test2",
                Quantity = 2
            };

            var basketReturn = new BasketModel
            {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                BasketItems = new List<BasketItemModel> { basketItem1, basketItem2 }
            };

            var discountReturn = new DiscountModel
            {
                ActiveFrom = DateTime.MinValue,
                ActiveTo = DateTime.MaxValue,
                MinimumSpend = 10,
                Amount = 10,
                Code = "DiscountTest",
                DiscountType = DiscountTypeEnum.GiftVoucher
            };

            var requestModel = new GetBasketModel
            {
                UserIdentifier = "test",
                CompanyIdentifier = "test",
                DiscountCode = "DiscountTest"
            };

            basketServiceMock.Setup(x => x.GetBasketForUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basketReturn);
            discountServiceMock.Setup(x => x.GetDiscountByCodeAndCompanyId(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(discountReturn);

            var result = await basketController.GetBasketForUser(requestModel);

            var typedResult = result as ObjectResult;
            var content = typedResult.Value as ProcessedBasketModel;


            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)typedResult.StatusCode);

            Assert.AreEqual(content.Total, 300);
            Assert.AreEqual(content.TotalWithDiscount, 290);
            Assert.AreEqual(content.BasketItems.Count(), 2);
        }

        [Test]
        public async Task Update_Basket_Null_Request_Object_Returns_404()
        {
            var result = await basketController.UpdateBasketForUser(null);

            var typedResult = result as ObjectResult;

            Assert.IsAssignableFrom<BadRequestObjectResult>(result);

            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)typedResult.StatusCode);
            Assert.AreEqual(typedResult.Value, "update model must not be null");
        }

        [Test]
        public async Task Update_Basket_Valid_Request_Object_Returns_Ok()
        {
            var basketItem = new BasketItemModel
            {
                Price = 50m,
                ProductCode = "test2",
                Quantity = 2
            };

            var basketReturn = new BasketModel
            {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                BasketItems = new List<BasketItemModel> { basketItem }
            };

            basketServiceMock.Setup(x => x.UpdateBasket(It.IsAny<UpdateBasketItemModel>())).ReturnsAsync(basketReturn);

            var basketUpdateRequest = new UpdateBasketItemModel
            {
                UserIdentifier = "test",
                CompanyIdentifier = "test",
                Price = 1m,
                ProductCode = "test",
                Quantity = 1
            };

            var result = await basketController.UpdateBasketForUser(basketUpdateRequest);

            var typedResult = result as ObjectResult;

            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.IsAssignableFrom<BasketModel>(typedResult.Value);

            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)typedResult.StatusCode);
        }

        [Test]
        public async Task Update_Multiple_Basket_Null_Request_Object_Returns_404()
        {
            var result = await basketController.UpdateMultipleBasketForUser(null);

            var typedResult = result as ObjectResult;

            Assert.IsAssignableFrom<BadRequestObjectResult>(result);

            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)typedResult.StatusCode);
            Assert.AreEqual(typedResult.Value, "update multiple model must not be null");
        }

        [Test]
        public async Task Update_Multiple_Basket_Items_Valid_Request_Object_Returns_Ok()
        {
            var basketItem = new BasketItemModel
            {
                Price = 50m,
                ProductCode = "test2",
                Quantity = 2
            };

            var basketReturn = new BasketModel
            {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                BasketItems = new List<BasketItemModel> { basketItem }
            };

            var basketUpdateItemRequest = new UpdateBasketItemInListModel
            {
                Price = 1m,
                ProductCode = "test",
                Quantity = 1
            };

            var basketUpdateRequest = new UpdateMultipleBasketItemModel
            {
                UserIdentifier = "test",
                CompanyIdentifier = "test",
                BasketItems = new List<UpdateBasketItemInListModel> { basketUpdateItemRequest }
            };

            basketServiceMock.Setup(x => x.UpdateMultipleBasketItem(It.IsAny<UpdateMultipleBasketItemModel>())).ReturnsAsync(basketReturn);

            var result = await basketController.UpdateMultipleBasketForUser(basketUpdateRequest);

            var typedResult = result as ObjectResult;

            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.IsAssignableFrom<BasketModel>(typedResult.Value);

            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)typedResult.StatusCode);
        }
    }
}