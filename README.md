# CaptchaCore: Captcha Verification in ASP.NET Core
## Get started with CaptchaCore in ASP.NET Core

### Package installation

- Go to View > Other Windows > Package Manager Console
- Execute the following command:
```
Install-Package CaptchaCore -Version 1.0.0
```

### Add and configure</h4>

- Add the CaptchaCore to the services collection in the Startup.ConfigureServices method:
```cs
        public void ConfigureServices(IServiceCollection services)
        {
            // This is add CaptchaCore with default settings
            services.AddCaptchaCore();
            services.AddControllers();
        }
```
- Or:
```cs
        public void ConfigureServices(IServiceCollection services)
        {
            // This is add CaptchaCore with customized settings
            services.AddCaptchaCore(new CaptchaSettings
            {
                Issuer = "YourApplicationName",
                CryptoKey = "thisisdefaultcryptokey",
                ExpirationInSeconds = 300,
                PermittedLetters = "0123456789",
                CodeLength = 5,
            });
            services.AddControllers();
        }
```

### Usage
- Inject :
```cs
        private readonly ICaptchaProvider _provider;

        public WeatherForecastController(ICaptchaProvider provider)
        {
            _provider = provider;
        }
```
- Generate :
```cs
var captcha = _provider.Generate(width: 500, height: 250);
Console.WriteLine($"This is client key : {captcha.ClientKey}");
Console.WriteLine($"This is captcha code : {captcha.Code}");
Console.WriteLine($"This is captcha image : {captcha.Image}");
```
- Verify :
```cs
var result = _provider.Verify(input, key);
return result.Succeeded;
```
