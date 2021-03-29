using Bogus;
using CaptchaCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaptchaCore.UnitTests.Models
{
    [TestClass]
    public class CaptchaResultTests
    {
        [TestMethod]
        public void Success_SucceededShouldBeTrue()
        {
            // Arrange

            // Act
            var result = CaptchaResult.Success();

            // Assert
            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public void Success_ErrorsShouldBeNull()
        {
            // Arrange

            // Act
            var result = CaptchaResult.Success();

            // Assert
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void Fail_SucceededShouldBeFalse()
        {
            // Arrange
            var errors = new Faker<CaptchaError>()
                .CustomInstantiator(e => new CaptchaError("", ""))
                .Generate(5);

            // Act
            var result = CaptchaResult.Fail(errors);

            // Assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void Fail_ErrorsShouldNotBeNull()
        {
            // Arrange
            var errors = new Faker<CaptchaError>()
                .CustomInstantiator(e => new CaptchaError("", ""))
                .Generate(5);

            // Act
            var result = CaptchaResult.Fail(errors);

            // Assert
            Assert.IsNotNull(result.Errors);
        }
    }
}