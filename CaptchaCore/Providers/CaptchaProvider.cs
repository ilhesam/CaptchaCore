using System;
using System.Collections.Generic;
using CaptchaCore.Models;
using CaptchaCore.Providers.CodeGenerator;
using CaptchaCore.Providers.Crypto;
using CaptchaCore.Providers.ErrorDescriber;
using CaptchaCore.Providers.ImageCreator;
using CaptchaCore.Providers.ObjectSerializer;
using CaptchaCore.Settings;
using Newtonsoft.Json;

namespace CaptchaCore.Providers
{
    public class CaptchaProvider : ICaptchaProvider
    {
        protected readonly CaptchaSettings Settings;
        protected readonly ICaptchaCodeGenerator CodeGenerator;
        protected readonly ICaptchaImageCreator ImageCreator;
        protected readonly ICaptchaCrypto Crypto;
        protected readonly ICaptchaObjectSerializer ObjectSerializer;
        protected readonly ICaptchaErrorDescriber ErrorDescriber;

        public CaptchaProvider(CaptchaSettings settings, ICaptchaCodeGenerator codeGenerator, ICaptchaImageCreator imageCreator, ICaptchaCrypto crypto, ICaptchaObjectSerializer objectSerializer, ICaptchaErrorDescriber errorDescriber)
        {
            Settings = settings;
            CodeGenerator = codeGenerator;
            ImageCreator = imageCreator;
            Crypto = crypto;
            ObjectSerializer = objectSerializer;
            ErrorDescriber = errorDescriber;
        }

        public virtual GenerateCaptchaResult Generate(CaptchaClientCredential credential, int width, int height)
        {
            var code = credential.Code;
            var image = ImageCreator.Create(code, width, height);
            var json = ObjectSerializer.Serialize(credential);
            var clientKey = Crypto.Encrypt(json, Settings.CryptoKey);

            return new GenerateCaptchaResult(clientKey, code, image);
        }

        public virtual GenerateCaptchaResult Generate(int width, int height)
        {
            var code = CodeGenerator.Generate(Settings.CodeLength, Settings.PermittedLetters);
            var credential = new CaptchaClientCredential
            {
                UniqueIdentifier = Guid.NewGuid().ToString(),
                Issuer = Settings.Issuer,
                Code = code,
                ExpiredAt = DateTime.Now.AddSeconds(Settings.ExpirationInSeconds)
            };

            return Generate(credential, width, height);
        }

        public virtual CaptchaResult Verify(string clientInput, string clientKey)
        {
            try
            {
                var json = Crypto.Decrypt(clientKey, Settings.CryptoKey);

                if (json == null)
                {
                    throw new Exception();
                }

                var captcha = ObjectSerializer.Deserialize(json);

                if (captcha == null)
                {
                    throw new Exception();
                }

                var errors = new List<CaptchaError>();

                if (captcha.Issuer != Settings.Issuer)
                {
                    errors.Add(ErrorDescriber.IssuerNotValid());
                }

                if (captcha.Code != clientInput)
                {
                    errors.Add(ErrorDescriber.CodeNotMatch());
                }

                if (captcha.ExpiredAt.CompareTo(DateTime.Now) == -1)
                {
                    errors.Add(ErrorDescriber.Expired());
                }

                if (errors.Count > 0)
                {
                    return CaptchaResult.Fail(errors);
                }

                return CaptchaResult.Success();
            }
            catch (Exception e) when (e is FormatException || e is JsonReaderException)
            {
                return CaptchaResult.Fail(new List<CaptchaError>
                {
                    ErrorDescriber.ClientKeyNotValid()
                });
            }
            catch
            {
                return CaptchaResult.Fail(new List<CaptchaError>
                {
                    ErrorDescriber.UnhandledError()
                });
            }
        }
    }
}