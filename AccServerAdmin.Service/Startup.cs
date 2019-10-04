using System;
using System.IO;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using AccServerAdmin.Persistence.EventConfig;
using AccServerAdmin.Persistence.GameConfig;
using AccServerAdmin.Persistence.Repository;
using AccServerAdmin.Persistence.ServerConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

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
            ConfigureDatabase(services);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages()
                .AddRazorPagesOptions(o =>
                {
                    o.Conventions.AuthorizePage("/Index");
                });

            RegisterApplicationComponents(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "AccServerAdmin");

            if (!Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);

            dbPath = Path.Combine(dbPath, "AccServerAdmin.db3");

            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite($"DataSource={dbPath}"));
            services.AddDefaultIdentity<IdentityUser>(o => o.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            var provider = services.BuildServiceProvider();
            var dbContext = provider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }

        private void RegisterApplicationComponents(IServiceCollection services)
        {
            // Application components
            services.AddTransient<IJsonConverter, JsonDotNetConverter>();
            services.AddTransient<IFile, FileApiWrapper>();
            services.AddTransient<IDirectory, DirectoryApiWrapper>();
            services.AddTransient<IServerDirectoryResolver, ServerDirectoryResolver>();
            
            // Repositories
            services.AddScoped<IServerRepository, ServerRepository>();
            services.AddScoped<IDataRepository<AppSettings>, AppSettingsRepository>();
            services.AddScoped<IDataRepository<GameConfiguration>, DataRepository<GameConfiguration>>();
            

            services.AddTransient<IConfigRepository<NetworkConfiguration>, NetworkConfigRepository>();
            services.AddTransient<IConfigRepository<GameConfiguration>, GameConfigRepository>();
            services.AddTransient<IConfigRepository<EventConfiguration>, EventConfigRepository>();

            // CQRS components
            services.AddScoped<IGetAppSettingsQuery, GetAppSettingsQuery>();
            services.AddScoped<ISaveAppSettingsCommand, SaveAppSettingsCommand>();
            services.AddTransient<ICreateServerCommand, CreateServerCommand>();
            services.AddTransient<IUpdateServerCommand, UpdateServerCommand>();
            //services.AddTransient<IDeleteServerCommand, DeleteServerCommand>();
            services.AddScoped<IGetServerListQuery, GetServerListQuery>();
            services.AddTransient<IGetServerByIdQuery, GetServerByIdQuery>();

            //services.AddScoped<IGetConfigByIdQuery<ServerConfiguration>, GetConfigByIdQuery<ServerConfiguration>>();
            //services.AddScoped<ISaveConfigCommand<ServerConfiguration>, SaveConfigCommand<ServerConfiguration>>();
            //services.AddScoped<IGetConfigByIdQuery<GameConfiguration>, GetConfigByIdQuery<GameConfiguration>>();
            //services.AddScoped<ISaveConfigCommand<GameConfiguration>, SaveConfigCommand<GameConfiguration>>();
            //services.AddScoped<IGetConfigByIdQuery<EventConfiguration>, GetConfigByIdQuery<EventConfiguration>>();
            //services.AddScoped<ISaveConfigCommand<EventConfiguration>, SaveConfigCommand<EventConfiguration>>();


        }
    }
}
