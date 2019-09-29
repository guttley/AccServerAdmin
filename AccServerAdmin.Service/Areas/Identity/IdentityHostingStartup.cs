using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AccServerAdmin.Service.Areas.Identity.IdentityHostingStartup))]
namespace AccServerAdmin.Service.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}