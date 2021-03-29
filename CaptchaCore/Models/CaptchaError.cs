namespace CaptchaCore.Models
{
    /// <summary>
    /// Used to captcha errors
    /// </summary>
    public class CaptchaError
    {
        public CaptchaError(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }
}