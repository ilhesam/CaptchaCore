using System.Drawing;
using CaptchaCore.Models;

namespace CaptchaCore.Providers
{
    /// <summary>
    /// Provides methods related to captcha
    /// </summary>
    public interface ICaptchaProvider
    {
        /// <summary>
        /// Generates captcha with customized credential
        /// </summary>
        /// <param name="credential">Customized credential</param>
        /// <param name="width">Width of captcha image</param>
        /// <param name="height">Height of captcha image</param>
        /// <returns>Info of generated captcha</returns>
        GenerateCaptchaResult Generate(CaptchaClientCredential credential, int width, int height);

        /// <summary>
        /// Generates captcha
        /// </summary>
        /// <param name="width">Width of captcha image</param>
        /// <param name="height">Height of captcha image</param>
        /// <returns>Info of generated captcha</returns>
        GenerateCaptchaResult Generate(int width, int height);

        /// <summary>
        /// Verifies captcha
        /// </summary>
        /// <param name="clientInput">Captcha code entered by the user</param>
        /// <param name="clientKey">Key sent by client</param>
        /// <returns>Result of captcha verification</returns>
        CaptchaResult Verify(string clientInput, string clientKey);
    }
}