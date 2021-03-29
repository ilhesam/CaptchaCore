using System;
using Bogus;
using CaptchaCore.Models;
using CaptchaCore.Providers.ObjectSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CaptchaCore.UnitTests.Providers.ObjectSerializer
{
    [TestClass]
    public class CaptchaObjectSerializerTests
    {
        [TestMethod]
        public void Serialize_ResultShouldNotNull()
        {
            // Arrange
            var input = new Faker<CaptchaClientCredential>()
                .RuleFor(e => e.UniqueIdentifier, Guid.NewGuid().ToString)
                .RuleFor(e => e.Issuer, e => e.Company.CompanyName())
                .RuleFor(e => e.Code, e => e.Random.Int(10000, 10019).ToString())
                .RuleFor(e => e.ExpiredAt, DateTime.Now)
                .Generate();
            var serializer = new Faker<CaptchaObjectSerializer>()
                .Generate();

            // Act
            var result = serializer.Serialize(input);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serialize_InputIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var serializer = new Faker<CaptchaObjectSerializer>()
                .Generate();

            // Act
            serializer.Serialize(null);

            // Assert
        }

        [TestMethod]
        public void Deserialize_ResultShouldNotBeNull()
        {
            // Arrange
            var input = new Faker<CaptchaClientCredential>()
                .RuleFor(e => e.UniqueIdentifier, Guid.NewGuid().ToString)
                .RuleFor(e => e.Issuer, e => e.Company.CompanyName())
                .RuleFor(e => e.Code, e => e.Random.Int(10000, 10019).ToString())
                .RuleFor(e => e.ExpiredAt, DateTime.Now)
                .Generate();
            var serializer = new Faker<CaptchaObjectSerializer>()
                .Generate();

            // Act
            var serializedInput = serializer.Serialize(input);
            var result = serializer.Deserialize(serializedInput);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Deserialize_SerializedInputIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var serializer = new Faker<CaptchaObjectSerializer>()
                .Generate();

            // Act
            serializer.Deserialize(null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void Deserialize_SerializedInputIsNotCorrectFormat_ThrowsJsonReaderException()
        {
            // Arrange
            var faker = new Faker();
            var serializer = new Faker<CaptchaObjectSerializer>()
                .Generate();
            var serializedInput = faker.Lorem.Text();

            // Act
            serializer.Deserialize(serializedInput);

            // Assert
        }
    }
}