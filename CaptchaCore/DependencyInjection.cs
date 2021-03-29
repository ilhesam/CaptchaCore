using CaptchaCore.Providers.CodeGenerator;
using CaptchaCore.Providers.Crypto;
using CaptchaCore.Providers.ErrorDescriber;
using CaptchaCore.Providers.ImageCreator;
using CaptchaCore.Providers.ObjectSerializer;
using CaptchaCore.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace CaptchaCore
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds CaptchaCore with default settings
        /// </summary>
        public static IServiceCollection AddCaptchaCore(this IServiceCollection services)
        {
            services.AddTransient<ICaptchaCodeGenerator, CaptchaCodeGenerator>();
            services.AddTransient<ICaptchaCrypto, CaptchaCrypto>();
            services.AddTransient<ICaptchaErrorDescriber, CaptchaErrorDescriber>();
            services.AddTransient<ICaptchaImageCreator, CaptchaImageCreator>();
            services.AddTransient<ICaptchaObjectSerializer, CaptchaObjectSerializer>();

            return services;
        }

        /// <summary>
        /// Adds CaptchaCore with customized settings
        /// </summary>
        public static IServiceCollection AddCaptchaCore(this IServiceCollection services, CaptchaSettings settings)
        {
            services.AddScoped<CaptchaSettings>(e => settings);

            return services.AddCaptchaCore();
        }
    }
}