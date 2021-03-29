using System;

namespace CaptchaCore.Models
{
    /// <summary>
    /// Used to client identification and captcha validation
    /// </summary>
    public class CaptchaClientCredential
    {
        public string UniqueIdentifier { get; set; }
        public string Issuer { get; set; }
        public string Code { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}