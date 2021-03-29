using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace CaptchaCore.Extensions
{
    /// <summary>
    /// Generates random value of different types
    /// </summary>
    internal static class Randomizer
    {
        private static readonly Random Random;

        static Randomizer()
        {
            Random = new Random();
        }

        /// <summary>
        /// Generates a random number within a range
        /// </summary>
        public static int Number(int min, int max)
        {
            return Random.Next(min, max);
        }

        /// <summary>
        /// Generates a random string with given size and permitted letters.   
        /// </summary>
        public static string String(int size, string permittedLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            var builder = new StringBuilder(size);

            for (var i = 0; i < size; i++)
            {
                var letter = permittedLetters[Random.Next(permittedLetters.Length - 1)];
                builder.Append(letter);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates a random password <br/>
        /// 4 LowerCase + 4 Digits + 2 UpperCase  
        /// </summary>
        /// <returns>Random password</returns>
        public static string Password()
        {
            var passwordBuilder = new StringBuilder();

            // 4 Letters lower case   
            passwordBuilder.Append(String(4));

            // 4 Digits between 1000 and 1001  
            passwordBuilder.Append(Number(1000, 1001));

            // 2 Letters upper case  
            passwordBuilder.Append(String(2));
            return passwordBuilder.ToString();
        }
    }
}