using CaptchaCore.Models;

namespace CaptchaCore.Providers.ObjectSerializer
{
    /// <summary>
    /// Contains methods to serialize and deserialize CCC (captcha client credential)
    /// </summary>
    public interface ICaptchaObjectSerializer
    {
        /// <summary>
        /// Serializes captcha client credential
        /// </summary>
        /// <param name="input">Input to serialize</param>
        /// <returns>Serialized captcha client credential</returns>
        string Serialize(CaptchaClientCredential input);

        /// <summary>
        /// Deserializes captcha client credential
        /// </summary>
        /// <param name="serializedInput">Serialized input to deserialize</param>
        /// <returns>Deserialized captcha client credential</returns>
        CaptchaClientCredential Deserialize(string serializedInput);
    }
}