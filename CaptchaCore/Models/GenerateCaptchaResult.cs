using System.Drawing;

namespace CaptchaCore.Models
{
    /// <summary>
    /// Returns required properties after captcha generated
    /// </summary>
    public class GenerateCaptchaResult
    {
        public GenerateCaptchaResult(string clientKey, string code, Bitmap image)
        {
            ClientKey = clientKey;
            Code = code;
            Image = image;
        }

        /// <summary>
        /// This key used to client identification and captcha validation <br/>
        /// SEND THIS VALUE TO CLIENT SIDE!
        /// </summary>
        public string ClientKey { get; set; }

        /// <summary>
        /// Generated captcha code <br/>
        /// DO NOT SEND THIS VALUE TO CLIENT SIDE!
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Created captcha image 
        /// </summary>
        public Bitmap Image { get; set; }
    }
}