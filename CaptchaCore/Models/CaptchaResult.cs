using System.Collections.Generic;

namespace CaptchaCore.Models
{
    /// <summary>
    /// Used to captcha results
    /// </summary>
    public class CaptchaResult
    {
        /// <summary>
        /// Constructs succeeded result
        /// </summary>
        public CaptchaResult()
        {
            Succeeded = true;
            Errors = null;
        }

        /// <summary>
        /// Constructs failed result
        /// </summary>
        public CaptchaResult(IList<CaptchaError> errors)
        {
            Succeeded = false;
            Errors = errors;
        }

        public bool Succeeded { get;}
        public IList<CaptchaError> Errors { get; }

        /// <summary>
        /// Returns succeeded result
        /// </summary>
        /// <returns>Succeeded result</returns>
        public static CaptchaResult Success() => new CaptchaResult();

        /// <summary>
        /// Returns failed result
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Failed result</returns>
        public static CaptchaResult Fail(IList<CaptchaError> errors) => new CaptchaResult(errors);
    }
}