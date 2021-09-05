using _288.TechTest.Data.Entities;
using _288.TechTest.Data.Interfaces;
using _288.TechTest.Domain.Models;
using _288.TechTest.Domain.Services;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _288.TechTest.Domain.Tests
{
    public class BasketServiceTests
    {
        private Mock<IBasketRepo> basketRepoMock;
        private IMapper mapper = TestMapper.Configure().CreateMapper();
        private BasketService basketService;

        [SetUp]
        public void Setup()
        {
            basketRepoMock = new Mock<IBasketRepo>(MockBehavior.Strict);
            basketService = new BasketService(basketRepoMock.Object, mapper);
        }

        [TearDown]
        public void Down()
        {
            basketRepoMock.VerifyAll();
        }

        [Test]
        [TestCase("", "")]
        [TestCase("test", "")]
        [TestCase("test", null)]
        [TestCase(null, null)]
        public async Task Get_Basket_For_User_Null_Parameters_Throws_Argument_Exception(string user, string company)
        {
            Assert.ThrowsAsync<ArgumentException>(() => basketService.GetBasketForUser(user, company));
        }

        [Test]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("test", null)]
        [TestCase("test", "")]
        public async Task Update_Basket_For_User_Null_Parameters_Throws_Argument_Exception(string user, string company)
        {
            Assert.ThrowsAsync<ArgumentException>(() => basketService.UpdateBasket(new UpdateBasketItemModel()));
        }

        [Test]
        public async Task Update_Basket_For_User_Null_Basket_Model_Throws_Argument_Null_Exception()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => basketService.UpdateBasket(null));
        }

        [Test]
        public async Task Get_Basket_For_User()
        {
            var basket = new Basket {
                CompanyIdentifier = "test",
                UserIdentifier = "test"
            };

            basketRepoMock.Setup(repo => repo.GetUsersBasket(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basket);
            var basketResponse = await basketService.GetBasketForUser("test", "test");

            Assert.AreEqual(basket.UserIdentifier, basketResponse.UserIdentifier);
            Assert.AreEqual(basket.CompanyIdentifier, basketResponse.CompanyIdentifier);
        }

        [Test]
        public async Task Get_Basket_For_User_Returns_Null_Does_Not_Throw_Exception()
        {
            var basket = (Basket)null;

            var createdBasket = new Basket();

            basketRepoMock.Setup(repo => repo.GetUsersBasket(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basket);
            basketRepoMock.Setup(repo => repo.CreateUserBasket(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(createdBasket);
            var basketResponse = await basketService.GetBasketForUser("test", "test");

            Assert.IsNotNull(basketResponse);
        }

        [Test]
        public async Task Update_Basket_For_User_User_Has_Basket()
        {
            var basketItem = new BasketItem
            {
                Price = 1m,
                ProductCode = "test",
                Quantity = 1
            };

            var basket = new Basket
            {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                BasketItems = new List<BasketItem>() { basketItem }
            };

            var updateBasketItem = new UpdateBasketItemModel
            {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                Price = 1m,
                ProductCode = "test",
                Quantity = 1
            };

            basketRepoMock.Setup(repo => repo.GetUsersBasket(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basket);
            basketRepoMock.Setup(repo => repo.UpdateBasketItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<BasketItem>())).ReturnsAsync(basket);

            var basketResponse = await basketService.UpdateBasket(updateBasketItem);

            Assert.IsNotNull(basketResponse);
            Assert.AreEqual(basket.UserIdentifier, basketResponse.UserIdentifier);
            Assert.AreEqual(basket.CompanyIdentifier, basketResponse.CompanyIdentifier);
        }

        [Test]
        public async Task Update_Basket_For_User_User_Does_Not_Have_Basket()
        {
            var basketNull = (Basket)null;

            var basketItem = new BasketItem
            {
                Price = 1m,
                ProductCode = "test",
                Quantity = 1
            };

            var basket = new Basket
            {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                BasketItems = new List<BasketItem>() { basketItem }
            };

            var updateBasketItem = new UpdateBasketItemModel {
                CompanyIdentifier = "test",
                UserIdentifier = "test",
                Price = 1m,
                ProductCode = "test",
                Quantity = 1
            };

            basketRepoMock.Setup(repo => repo.GetUsersBasket(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basketNull);
            basketRepoMock.Setup(repo => repo.CreateUserBasket(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basket);
            basketRepoMock.Setup(repo => repo.UpdateBasketItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<BasketItem>())).ReturnsAsync(basket);

            var basketResponse = await basketService.UpdateBasket(updateBasketItem);

            Assert.IsNotNull(basketResponse);
            Assert.AreEqual(basket.UserIdentifier, basketResponse.UserIdentifier);
            Assert.AreEqual(basket.CompanyIdentifier, basketResponse.CompanyIdentifier);
        }

        [Test]
        public async Task Update_Multiple_Basket_Items_For_User_User_Does_Not_Have_Basket()
        {
            var basketNull = (Basket)null;

            var updateModel = new UpdateMultipleBasketItemModel {
                CompanyIdentifier = "test",
                UserIdentifier = "test"
            };

                var basketItem1 = new UpdateBasketItemInListModel
            {
                Price = 1m,
                ProductCode = "test1",
                Quantity = 1
            };

            var basketItem2 = new UpdateBasketItemInListModel
            {
                Price = 1m,
                ProductCode = "test2",
                Quantity = 1
            };

            var basket = new Basket
            {
                CompanyIdentifier = "test",
                UserIdentifier = "test"
            };


            var updateBasketItem = new List<UpdateBasketItemInListModel> { basketItem1, basketItem2 };

            updateModel.BasketItems = updateBasketItem;

            basketRepoMock.Setup(repo => repo.GetUsersBasket(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basketNull);
            basketRepoMock.Setup(repo => repo.CreateUserBasket(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(basket);
            basketRepoMock.Setup(repo => repo.UpdateMultipleBasketItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<BasketItem>>())).ReturnsAsync(basket);

            var basketResponse = await basketService.UpdateMultipleBasketItem(updateModel);

            Assert.IsNotNull(basketResponse);
            Assert.AreEqual(basket.UserIdentifier, basketResponse.UserIdentifier);
            Assert.AreEqual(basket.CompanyIdentifier, basketResponse.CompanyIdentifier);
        }
    }
}