namespace CaptchaCore.Providers.CodeGenerator
{
    /// <summary>
    /// Contains methods to generate captcha code
    /// </summary>
    public interface ICaptchaCodeGenerator
    {
        /// <summary>
        /// Generates captcha code
        /// </summary>
        /// <param name="codeLength">Specifies length of captcha code</param>
        /// <param name="permittedLetters">Specifies permitted letters of captcha code</param>
        /// <returns>Generated captcha code</returns>
        string Generate(int codeLength, string permittedLetters);
    }
}