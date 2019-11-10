using FluffySpoon.AspNet.LetsEncrypt;
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
#if (RELEASE)
                        .UseKestrel(kestrelOptions =>
                        {
                            kestrelOptions.ConfigureHttpsDefaults(httpsOptions =>
                            {
                                httpsOptions.ServerCertificateSelector = (c, s) => LetsEncryptRenewalService.Certificate;
                            });
                        })
                        .UseUrls(
                            $"http://{DomainToUse}",
                            $"https://{DomainToUse}")
#endif
                        .UseStartup<Startup>();
                });
    }
}
