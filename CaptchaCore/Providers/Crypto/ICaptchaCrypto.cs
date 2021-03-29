namespace CaptchaCore.Providers.Crypto
{
    /// <summary>
    /// Contains methods to encryption and decryption captcha
    /// </summary>
    public interface ICaptchaCrypto
    {
        /// <summary>
        /// Encrypts input using key
        /// </summary>
        /// <param name="input">Input to encrypt</param>
        /// <param name="key">Encryption key</param>
        /// <returns>Encrypted input</returns>
        string Encrypt(string input, string key);

        /// <summary>
        /// Decrypts input using key
        /// </summary>
        /// <param name="encryptedInput">Encrypted input to decrypt</param>
        /// <param name="key">Decryption key</param>
        /// <returns>Decrypted input</returns>
        string Decrypt(string encryptedInput, string key);
    }
}