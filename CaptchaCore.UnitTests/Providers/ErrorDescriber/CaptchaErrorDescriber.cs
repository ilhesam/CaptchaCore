using Bogus;
using CaptchaCore.Providers.ErrorDescriber;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaptchaCore.UnitTests.Providers.ErrorDescriber
{
    [TestClass]
    public class CaptchaErrorDescriberTests
    {
        [TestMethod]
        public void ClientKeyNotValid_ReturnsCaptchaError()
        {
            // Arrange
            var describer = new Faker<CaptchaErrorDescriber>()
                .Generate();

            // Act
            var result = describer.ClientKeyNotValid();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IssuerNotValid_ReturnsCaptchaError()
        {
            // Arrange
            var describer = new Faker<CaptchaErrorDescriber>()
                .Generate();

            // Act
            var result = describer.IssuerNotValid();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CodeNotMatch_ReturnsCaptchaError()
        {
            // Arrange
            var describer = new Faker<CaptchaErrorDescriber>()
                .Generate();

            // Act
            var result = describer.CodeNotMatch();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Expired_ReturnsCaptchaError()
        {
            // Arrange
            var describer = new Faker<CaptchaErrorDescriber>()
                .Generate();

            // Act
            var result = describer.Expired();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UnhandledError_ReturnsCaptchaError()
        {
            // Arrange
            var describer = new Faker<CaptchaErrorDescriber>()
                .Generate();

            // Act
            var result = describer.UnhandledError();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}