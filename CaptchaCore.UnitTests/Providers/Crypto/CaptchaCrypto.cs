using System;
using Bogus;
using CaptchaCore.Providers.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaptchaCore.UnitTests.Providers.Crypto
{
    [TestClass]
    public class CaptchaCryptoTests
    {
        [TestMethod]
        public void Encrypt_ResultShouldNotBeNull()
        {
            // Arrange
            var faker = new Faker();
            var key = faker.Random.Hash();
            var input = faker.Lorem.Text();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            var result = crypto.Encrypt(input, key);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_InputIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var key = faker.Random.Hash();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            crypto.Encrypt(null, key);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_InputIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var key = faker.Random.Hash();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            crypto.Encrypt(string.Empty, key);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_KeyIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var input = faker.Lorem.Text();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            crypto.Encrypt(input, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_KeyIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var input = faker.Lorem.Text();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            crypto.Encrypt(input, string.Empty);

            // Assert
        }

        [TestMethod]
        public void Decrypt_ResultShouldNotBeNull()
        {
            // Arrange
            var faker = new Faker();
            var key = faker.Random.Hash();
            var input = faker.Lorem.Text();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            var encryptedText = crypto.Encrypt(input, key);
            var result = crypto.Decrypt(encryptedText, key);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decrypt_EncryptedInputIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var key = faker.Random.Hash();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            var result = crypto.Decrypt(null, key);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decrypt_EncryptedInputIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            var faker = new Faker();
            var key = faker.Random.Hash();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            var result = crypto.Decrypt(string.Empty, key);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decrypt_KeyIsNull_ResultShouldNotBeNull()
        {
            // Arrange
            var faker = new Faker();
            var input = faker.Lorem.Text();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            var result = crypto.Decrypt(input, null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decrypt_KeyIsEmpty_ResultShouldNotBeNull()
        {
            // Arrange
            var faker = new Faker();
            var input = faker.Lorem.Text();
            var crypto = new Faker<CaptchaCrypto>()
                .Generate();

            // Act
            var result = crypto.Decrypt(input, string.Empty);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}