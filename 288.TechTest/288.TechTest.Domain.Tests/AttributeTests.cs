using _288.TechTest.Api.Attributes;
using NUnit.Framework;

namespace _288.TechTest.Domain.Tests
{
    public class AttributeTests
    {
        [Test]
        public void Not_Equal_Attribute_Test_Not_Equal_Success()
        {
            // arrange
            var value = 1;
            var attrib = new NotEqualToAttribute(0);

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public void Not_Equal_Attribute_Test_Not_Equal_Failure()
        {
            // arrange
            var value = 1;
            var attrib = new NotEqualToAttribute(1);

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }
    }
}
