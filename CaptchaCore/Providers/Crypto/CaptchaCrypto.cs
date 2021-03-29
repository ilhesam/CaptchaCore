using System;
using CaptchaCore.Settings;

namespace CaptchaCore.Providers.Crypto
{
    public class CaptchaCrypto : ICaptchaCrypto
    {
        public CaptchaCrypto()
        {
        }

        public virtual string Encrypt(string input, string key)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return AesCrypto.EncryptText(input, key);
        }

        public virtual string Decrypt(string encryptedInput, string key)
        {
            if (string.IsNullOrEmpty(encryptedInput))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return AesCrypto.DecryptText(encryptedInput, key);
        }
    }
}