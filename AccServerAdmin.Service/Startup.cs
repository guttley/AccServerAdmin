using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.EventConfig;
using AccServerAdmin.Persistence.GameConfig;
using AccServerAdmin.Persistence.Server;
using AccServerAdmin.Persistence.ServerConfig;
using AccServerAdmin.Service.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZNetCS.AspNetCore.Authentication.Basic;

namespace AccServerAdmin.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddControllersWithViews();
            services.AddScoped<AuthenticationEvents>();

            services
                .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication(
                    options =>
                    {
                        options.Realm = "ACC Server Admin";
                        options.EventsType = typeof(AuthenticationEvents);
                    });

            RegisterApplicationComponents(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthorization();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterApplicationComponents(IServiceCollection services)
        {
            // Application components
            services.AddTransient<IJsonConverter, JsonConverter>();
            services.AddTransient<IFile, FileApiWrapper>();
            services.AddTransient<IDirectory, DirectoryApiWrapper>();
            services.AddTransient<IServerDirectoryResolver, ServerDirectoryResolver>();
            services.AddTransient<ConfigValidator>();

            // Server components
            services.AddTransient<ICreateServerCommand, CreateServerCommand>();
            services.AddTransient<IUpdateServerCommand, UpdateServerCommand>();
            services.AddTransient<IDeleteServerCommand, DeleteServerCommand>();
            services.AddTransient<IGetServerListQuery, GetServerListQuery>();
            services.AddTransient<IGetServerByIdQuery, GetServerByIdQuery>();

            // Config components
            services.AddTransient<IGetConfigByIdQuery<ServerConfiguration>, GetConfigByIdQuery<ServerConfiguration>>();
            services.AddTransient<ISaveConfigCommand<ServerConfiguration>, SaveConfigCommand<ServerConfiguration>>();
            services.AddTransient<IGetConfigByIdQuery<GameConfiguration>, GetConfigByIdQuery<GameConfiguration>>();
            services.AddTransient<ISaveConfigCommand<GameConfiguration>, SaveConfigCommand<GameConfiguration>>();
            services.AddTransient<IGetConfigByIdQuery<EventConfiguration>, GetConfigByIdQuery<EventConfiguration>>();
            services.AddTransient<ISaveConfigCommand<EventConfiguration>, SaveConfigCommand<EventConfiguration>>();

            // repositories
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IConfigRepository<ServerConfiguration>, ServerConfigRepository>();
            services.AddTransient<IConfigRepository<GameConfiguration>, GameConfigRepository>();
            services.AddTransient<IConfigRepository<EventConfiguration>, EventConfigRepository>();

        }
    }
}
