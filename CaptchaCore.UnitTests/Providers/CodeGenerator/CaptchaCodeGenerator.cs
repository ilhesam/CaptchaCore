using System;
using System.Linq;
using Bogus;
using CaptchaCore.Providers.CodeGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaptchaCore.UnitTests.Providers.CodeGenerator
{
    [TestClass]
    public class CaptchaCodeGeneratorTests
    {
        [TestMethod]
        public void Generate_ReturnsRandomString()
        {
            // Arrange
            var faker = new Faker();
            var codeLength = faker.Random.Int(5, 10);
            var permittedChars = faker.Random.Chars(default, default, 3);
            var permittedLetters = new string(permittedChars);
            var generator = new Faker<CaptchaCodeGenerator>()
                .Generate();

            // Act
            var result = generator.Generate(codeLength, permittedLetters);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Generate_OnlyPermittedLettersInResult()
        {
            // Arrange
            var faker = new Faker();
            var codeLength = faker.Random.Int(5, 10);
            var permittedChars = faker.Random.Chars(default, default, 3);
            var permittedLetters = new string(permittedChars);
            var generator = new Faker<CaptchaCodeGenerator>()
                .Generate();

            // Act
            var result = generator.Generate(codeLength, permittedLetters);

            // Assert
            Assert.IsTrue(result.Any(e => permittedChars.Contains(e)));
        }

        [TestMethod]
        public void Generate_CodeLengthIsEqualToInput()
        {
            // Arrange
            var faker = new Faker();
            var codeLength = faker.Random.Int(5, 10);
            var permittedChars = faker.Random.Chars(default, default, 3);
            var permittedLetters = new string(permittedChars);
            var generator = new Faker<CaptchaCodeGenerator>()
                .Generate();

            // Act
            var result = generator.Generate(codeLength, permittedLetters);

            // Assert
            Assert.AreEqual(codeLength, result.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Generate_CodeLengthIsLessThan3_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();
            var codeLength = faker.Random.Int(2);
            var permittedChars = faker.Random.Chars(default, default, 3);
            var permittedLetters = new string(permittedChars);
            var generator = new Faker<CaptchaCodeGenerator>()
                .Generate();

            // Act
            generator.Generate(codeLength, permittedLetters);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Generate_CodeLengthIsGreaterThan10_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();
            var codeLength = faker.Random.Int(11);
            var permittedChars = faker.Random.Chars(default, default, 3);
            var permittedLetters = new string(permittedChars);
            var generator = new Faker<CaptchaCodeGenerator>()
                .Generate();

            // Act
            generator.Generate(codeLength, permittedLetters);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Generate_PermittedLettersIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var codeLength = faker.Random.Int(5, 10);
            var generator = new Faker<CaptchaCodeGenerator>()
                .Generate();

            // Act
            generator.Generate(codeLength, null);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Generate_PermittedLettersIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var codeLength = faker.Random.Int(5, 10);
            var generator = new Faker<CaptchaCodeGenerator>()
                .Generate();

            // Act
            generator.Generate(codeLength, string.Empty);

            // Assert
            Assert.IsTrue(true);
        }
    }
}