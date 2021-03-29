using CaptchaCore.Models;

namespace CaptchaCore.Providers.ErrorDescriber
{
    public class CaptchaErrorDescriber : ICaptchaErrorDescriber
    {
        public virtual CaptchaError ClientKeyNotValid()
            => new CaptchaError(nameof(ClientKeyNotValid), "Captcha key is not valid");

        public virtual CaptchaError IssuerNotValid()
            => new CaptchaError(nameof(IssuerNotValid), "Captcha issuer is not valid");

        public virtual CaptchaError CodeNotMatch()
            => new CaptchaError(nameof(CodeNotMatch), "Captcha code is not match");

        public virtual CaptchaError Expired()
            => new CaptchaError(nameof(Expired), "Captcha code is expired");

        public CaptchaError UnhandledError()
            => new CaptchaError(nameof(UnhandledError), "Unhandled error");
    }
}