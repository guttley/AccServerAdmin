using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AccServerAdmin.Service
{
    public class Program
    {
        public const string DomainToUse = "gs1.simsport-racing.com";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(kestrelOptions =>
                        {
#if (USE_HTTPS)
                            kestrelOptions.ConfigureHttpsDefaults(httpsOptions =>
                            {
                                httpsOptions.ServerCertificateSelector = (c, s) => LetsEncryptRenewalService.Certificate;
                            });
#endif
                        })
                        .UseUrls(
#if (USE_HTTPS)
                            $"http://{DomainToUse}",
                            $"https://{DomainToUse}")
#else 
                            "http://localhost:58080",
                            $"http://{DomainToUse}")
#endif
                        .UseStartup<Startup>();
                });
    }
}
