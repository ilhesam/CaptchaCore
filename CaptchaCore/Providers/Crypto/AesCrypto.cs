using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CaptchaCore.Providers.Crypto
{
    internal static class AesCrypto
    {
        private static byte[] Encrypt(byte[] bytesToEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (var memoryStream = new MemoryStream())
            {
                using var aes = new RijndaelManaged
                {
                    KeySize = 256,
                    BlockSize = 128
                };

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);
                aes.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToEncrypted, 0, bytesToEncrypted.Length);
                    cs.Close();
                }

                encryptedBytes = memoryStream.ToArray();
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (var memoryStream = new MemoryStream())
            {
                using var aes = new RijndaelManaged
                {
                    KeySize = 256,
                    BlockSize = 128
                };

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);
                aes.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToDecrypted, 0, bytesToDecrypted.Length);
                    cs.Close();
                }

                decryptedBytes = memoryStream.ToArray();
            }

            return decryptedBytes;
        }

        public static string EncryptText(string input, string password)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var bytesToEncrypted = Encoding.UTF8.GetBytes(input);

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = Encrypt(bytesToEncrypted, passwordBytes);
            var result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public static string DecryptText(string input, string password)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var bytesToDecrypted = Convert.FromBase64String(input);

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = Decrypt(bytesToDecrypted, passwordBytes);
            var result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }
    }
}