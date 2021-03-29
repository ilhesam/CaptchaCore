using System;
using System.Linq;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaptchaCore.UnitTests.Extensions
{
    [TestClass]
    public class RandomizerTests
    {
        [TestMethod]
        public void Number_ReturnsRandomNumber()
        {
            // Arrange
            var faker = new Faker();
            var min = faker.Random.Int();
            var max = faker.Random.Int(min);

            // Act
            var result = CaptchaCore.Extensions.Randomizer.Number(min, max);

            // Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Number_MinIsGreaterThanMax_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();
            var max = faker.Random.Int();
            var min = faker.Random.Int(max, default);

            // Act
            CaptchaCore.Extensions.Randomizer.Number(min, max);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void String_ReturnsRandomString()
        {
            // Arrange
            var faker = new Faker();
            var size = faker.Random.Int(50, 100);

            // Act
            var result = CaptchaCore.Extensions.Randomizer.String(size);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void String_SizeIsLessThan0_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();
            var size = faker.Random.Int(default, 0);

            // Act
            CaptchaCore.Extensions.Randomizer.String(size);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void String_PermittedLettersIsCustomized_OnlyPermittedLettersInResult()
        {
            // Arrange
            var faker = new Faker();
            var size = faker.Random.Int(50, 100);
            var permittedChars = faker.Random.Chars(default, default, 3);
            var permittedLetters = new string(permittedChars);

            // Act
            var result = CaptchaCore.Extensions.Randomizer.String(size, permittedLetters);

            // Assert
            Assert.IsTrue(result.Any(e => permittedChars.Contains(e)));
        }

        [TestMethod]
        public void Password_ReturnsRandomPassword()
        {
            // Arrange

            // Act
            var result = CaptchaCore.Extensions.Randomizer.Password();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}