using CaptchaCore.Extensions;

namespace CaptchaCore.Settings
{
    /// <summary>
    /// Specifies captcha settings
    /// </summary>
    public class CaptchaSettings
    {
        public string Issuer { get; set; } = "Default";
        public string CryptoKey { get; set; } = Randomizer.Password();
        public int ExpirationInSeconds { get; set; } = 300;
        public string PermittedLetters { get; set; } = "0123456789";
        public int CodeLength { get; set; } = 6;
    }
}