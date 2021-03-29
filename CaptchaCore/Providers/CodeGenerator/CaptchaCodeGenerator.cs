using System;
using CaptchaCore.Extensions;

namespace CaptchaCore.Providers.CodeGenerator
{
    public class CaptchaCodeGenerator : ICaptchaCodeGenerator
    {
        public virtual string Generate(int codeLength, string permittedLetters)
        {
            if (codeLength < 3 || codeLength > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(codeLength));
            }

            if (string.IsNullOrEmpty(permittedLetters))
            {
                throw new ArgumentNullException(nameof(permittedLetters));
            }

            return Randomizer.String(codeLength, permittedLetters);
        }
    }
}