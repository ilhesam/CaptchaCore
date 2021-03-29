using System;
using CaptchaCore.Models;
using Newtonsoft.Json;

namespace CaptchaCore.Providers.ObjectSerializer
{
    public class CaptchaObjectSerializer : ICaptchaObjectSerializer
    {
        public virtual string Serialize(CaptchaClientCredential input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(input);
        }

        public virtual CaptchaClientCredential Deserialize(string serializedInput)
        {
            if (string.IsNullOrEmpty(serializedInput))
            {
                throw new ArgumentNullException(nameof(serializedInput));
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<CaptchaClientCredential>(serializedInput);
        }
    }
}