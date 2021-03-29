using System;
using Bogus;
using CaptchaCore.Providers.ImageCreator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaptchaCore.UnitTests.Providers.ImageCreator
{
    [TestClass]
    public class CaptchaImageCreatorTests
    {
        [TestMethod]
        public void Create_ResultShouldNotBeNull()
        {
            // Arrange
            var faker = new Faker();
            var captchaCode = faker.Random.Int(10000, 10019).ToString();
            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);
            var creator = new Faker<CaptchaImageCreator>()
                .Generate();

            // Act
            var result = creator.Create(captchaCode, width, height);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_CaptchaCodeIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);
            var creator = new Faker<CaptchaImageCreator>()
                .Generate();

            // Act
            creator.Create(null, width, height);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_CaptchaCodeIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);
            var creator = new Faker<CaptchaImageCreator>()
                .Generate();

            // Act
            creator.Create(string.Empty, width, height);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_WidthIsLessThan10_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();
            var captchaCode = faker.Random.Int(10000, 10019).ToString();
            var width = faker.Random.Int(max: 9);
            var height = faker.Random.Int(100, 500);
            var creator = new Faker<CaptchaImageCreator>()
                .Generate();

            // Act
            creator.Create(captchaCode, width, height);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_WidthIsGreaterThan10000_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();
            var captchaCode = faker.Random.Int(10000, 10019).ToString();
            var width = faker.Random.Int(1001);
            var height = faker.Random.Int(100, 500);
            var creator = new Faker<CaptchaImageCreator>()
                .Generate();

            // Act
            creator.Create(captchaCode, width, height);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_HeightIsLessThan10_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();
            var captchaCode = faker.Random.Int(10000, 10019).ToString();
            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(max: 9);
            var creator = new Faker<CaptchaImageCreator>()
                .Generate();

            // Act
            var result = creator.Create(captchaCode, width, height);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_HeightIsGreaterThan10000_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();
            var captchaCode = faker.Random.Int(10000, 10019).ToString();
            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(1001);
            var creator = new Faker<CaptchaImageCreator>()
                .Generate();

            // Act
            var result = creator.Create(captchaCode, width, height);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}