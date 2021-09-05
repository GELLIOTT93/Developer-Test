using _288.TechTest.Data.Entities;
using _288.TechTest.Data.Interfaces;
using _288.TechTest.Domain.Services;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace _288.TechTest.Domain.Tests
{
    public class DiscountServiceTests
    {
        private Mock<IDiscountRepo> discountRepoMock;
        private IMapper mapper = TestMapper.Configure().CreateMapper();
        private DiscountService discountService;
        
        [SetUp]
        public void SetUp()
        {
            discountRepoMock = new Mock<IDiscountRepo>(MockBehavior.Strict);
            discountService = new DiscountService(mapper, discountRepoMock.Object);
        }

        [TearDown]
        public void Down()
        {
            discountRepoMock.VerifyAll();
        }

        [Test]
        [TestCase("", "")]
        [TestCase("test", "")]
        [TestCase("test", null)]
        [TestCase(null, null)]
        public async Task Get_Discount_For_Company_Null_Parameters_Throws_Argument_Exception(string code, string company)
        {
            Assert.ThrowsAsync<ArgumentException>(() => discountService.GetDiscountByCodeAndCompanyId(code, company));
        }

        [Test]
        public async Task Get_Discount_For_Company_Returns_Null_Does_Not_Throw_Exception()
        {
            var nullDiscount = (Discount)null;
            discountRepoMock.Setup(repo => repo.GetDiscountByCodeAndCustomerId(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(nullDiscount);
            var basketResponse = await discountService.GetDiscountByCodeAndCompanyId("test", "test");
            Assert.IsNull(basketResponse);
        }

    }
}
