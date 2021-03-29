using System;
using System.Linq;
using Bogus;
using CaptchaCore.Models;
using CaptchaCore.Providers;
using CaptchaCore.Providers.CodeGenerator;
using CaptchaCore.Providers.Crypto;
using CaptchaCore.Providers.ErrorDescriber;
using CaptchaCore.Providers.ImageCreator;
using CaptchaCore.Providers.ObjectSerializer;
using CaptchaCore.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaptchaCore.UnitTests.Providers
{
    [TestClass]
    public class CaptchaProviderTests
    {
        [TestMethod]
        public void Generate_ResultShouldNotBeNull()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            var result = provider.Generate(width, height);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ClientKey);
            Assert.IsNotNull(result.Code);
            Assert.IsNotNull(result.Image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Generate_WidthIsLessThan10_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(max: 10);
            var height = faker.Random.Int(100, 500);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            provider.Generate(width, height);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Generate_WidthIsGreaterThan10000_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(10001);
            var height = faker.Random.Int(100, 500);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            provider.Generate(width, height);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Generate_HeightIsLessThan10_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(max: 9);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            provider.Generate(width, height);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Generate_HeightIsGreaterThan10000_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(10001);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            provider.Generate(width, height);

            // Assert
        }

        [TestMethod]
        public void Verify_ResultShouldBeSucceeded()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            var generate = provider.Generate(width, height);
            var result = provider.Verify(generate.Code, generate.ClientKey);

            // Assert
            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void Verify_CodeIsNotMatch_ResultShouldNotBeSucceeded()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);
            var code = faker.Random.String(5);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            var generate = provider.Generate(width, height);
            var result = provider.Verify(code, generate.ClientKey);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.IsNotNull(result.Errors);
        }

        [TestMethod]
        public void Verify_CodeIsNotMatch_ErrorsShouldBeExpected()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);
            var code = faker.Random.String(5);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            var generate = provider.Generate(width, height);
            var result = provider.Verify(code, generate.ClientKey);

            // Assert
            Assert.IsTrue(result.Errors.Any(e => e.Code == nameof(CaptchaErrorDescriber.CodeNotMatch)));
        }

        [TestMethod]
        public void Verify_ClientKeyIsNotValid_ResultShouldNotBeSucceeded()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);
            var clientKey = faker.Lorem.Text();

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            var generate = provider.Generate(width, height);
            var result = provider.Verify(generate.Code, clientKey);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.IsNotNull(result.Errors);
        }

        [TestMethod]
        public void Verify_ClientKeyIsNotValid_ErrorsShouldBeExpected()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);
            var clientKey = Guid.NewGuid().ToString();

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            // Act
            var generate = provider.Generate(width, height);
            var result = provider.Verify(generate.Code, clientKey);

            // Assert
            Assert.IsTrue(result.Errors.Any(e => e.Code == nameof(CaptchaErrorDescriber.ClientKeyNotValid)));
        }

        [TestMethod]
        public void Verify_IssuerIsNotValid_ResultShouldNotBeSucceeded()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            var credential = new Faker<CaptchaClientCredential>()
                .RuleFor(e => e.UniqueIdentifier, Guid.NewGuid().ToString)
                .RuleFor(e => e.Issuer, e => e.Company.CompanyName())
                .RuleFor(e => e.Code, e => e.Random.Int(1000, 4000).ToString())
                .RuleFor(e => e.ExpiredAt, DateTime.Now.AddSeconds(settings.ExpirationInSeconds))
                .Generate();

            // Act
            var generate = provider.Generate(credential, width, height);
            var result = provider.Verify(generate.Code, generate.ClientKey);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.IsNotNull(result.Errors);
        }

        [TestMethod]
        public void Verify_IssuerIsNotValid_ErrorsShouldBeExpected()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            var credential = new Faker<CaptchaClientCredential>()
                .RuleFor(e => e.UniqueIdentifier, Guid.NewGuid().ToString)
                .RuleFor(e => e.Issuer, e => e.Company.CompanyName())
                .RuleFor(e => e.Code, e => e.Random.Int(1000, 4000).ToString())
                .RuleFor(e => e.ExpiredAt, DateTime.Now.AddSeconds(settings.ExpirationInSeconds))
                .Generate();

            // Act
            var generate = provider.Generate(credential, width, height);
            var result = provider.Verify(generate.Code, generate.ClientKey);

            // Assert
            Assert.IsTrue(result.Errors.Any(e => e.Code == nameof(CaptchaErrorDescriber.IssuerNotValid)));
        }

        [TestMethod]
        public void Verify_ExpiryDateHasPassed_ResultShouldNotBeSucceeded()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            var credential = new Faker<CaptchaClientCredential>()
                .RuleFor(e => e.UniqueIdentifier, Guid.NewGuid().ToString)
                .RuleFor(e => e.Issuer, settings.Issuer)
                .RuleFor(e => e.Code, e => e.Random.Int(1000, 4000).ToString())
                .RuleFor(e => e.ExpiredAt, e => e.Date.Recent())
                .Generate();

            // Act
            var generate = provider.Generate(credential, width, height);
            var result = provider.Verify(generate.Code, generate.ClientKey);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.IsNotNull(result.Errors);
        }

        [TestMethod]
        public void Verify_ExpiryDateHasPassed_ErrorsShouldBeExpected()
        {
            // Arrange
            var faker = new Faker();

            var width = faker.Random.Int(500, 1000);
            var height = faker.Random.Int(100, 500);

            var settings = new Faker<CaptchaSettings>().Generate();
            var codeGenerator = new Faker<CaptchaCodeGenerator>().Generate();
            var imageCreator = new Faker<CaptchaImageCreator>().Generate();
            var crypto = new Faker<CaptchaCrypto>().Generate();
            var objectSerializer = new Faker<CaptchaObjectSerializer>().Generate();
            var errorDescriber = new Faker<CaptchaErrorDescriber>().Generate();
            var provider = new Faker<CaptchaProvider>()
                .CustomInstantiator(e => new CaptchaProvider(settings, codeGenerator, imageCreator, crypto, objectSerializer, errorDescriber))
                .Generate();

            var credential = new Faker<CaptchaClientCredential>()
                .RuleFor(e => e.UniqueIdentifier, Guid.NewGuid().ToString)
                .RuleFor(e => e.Issuer, settings.Issuer)
                .RuleFor(e => e.Code, e => e.Random.Int(1000, 4000).ToString())
                .RuleFor(e => e.ExpiredAt, e => e.Date.Recent())
                .Generate();

            // Act
            var generate = provider.Generate(credential, width, height);
            var result = provider.Verify(generate.Code, generate.ClientKey);

            // Assert
            Assert.IsTrue(result.Errors.Any(e => e.Code == nameof(CaptchaErrorDescriber.Expired)));
        }
    }
}