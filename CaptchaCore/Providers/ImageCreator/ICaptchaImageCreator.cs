using System.Drawing;

namespace CaptchaCore.Providers.ImageCreator
{
    /// <summary>
    /// Contains methods to create captcha image
    /// </summary>
    public interface ICaptchaImageCreator
    {
        /// <summary>
        /// Creates captcha image
        /// </summary>
        /// <param name="captchaCode">Captcha code to write on image</param>
        /// <param name="width">Width of captcha image</param>
        /// <param name="height">Height of captcha image</param>
        /// <returns>Created captcha image</returns>
        Bitmap Create(string captchaCode, int width, int height);
    }
}