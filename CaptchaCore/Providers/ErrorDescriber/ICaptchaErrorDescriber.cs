using CaptchaCore.Models;

namespace CaptchaCore.Providers.ErrorDescriber
{
    /// <summary>
    /// Used to get captcha errors
    /// </summary>
    public interface ICaptchaErrorDescriber
    {
        /// <summary>
        /// Used when client key format is not correct
        /// </summary>
        /// <returns>Captcha error</returns>
        CaptchaError ClientKeyNotValid();
        
        /// <summary>
        /// Used when issuer is not match
        /// </summary>
        /// <returns>Captcha error</returns>
        CaptchaError IssuerNotValid();

        /// <summary>
        /// Used when captcha code is not match
        /// </summary>
        /// <returns>Captcha error</returns>
        CaptchaError CodeNotMatch();

        /// <summary>
        /// Used when captcha code is expired
        /// </summary>
        /// <returns>Captcha error</returns>
        CaptchaError Expired();

        /// <summary>
        /// Used when unhandled error is thrown
        /// </summary>
        /// <returns>Captcha error</returns>
        CaptchaError UnhandledError();
    }
}