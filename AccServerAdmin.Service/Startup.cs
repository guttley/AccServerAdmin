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
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(o => o.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
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

            dbContext.Database.Migrate();
        }

        private void RegisterApplicationComponents(IServiceCollection services)
        {
            // Application components
            services.AddTransient<IJsonConverter, JsonConverter>();
            services.AddTransient<IFile, FileApiWrapper>();
            services.AddTransient<IDirectory, DirectoryApiWrapper>();
            services.AddTransient<IServerDirectoryResolver, ServerDirectoryResolver>();
            
            // CQRS components
            services.AddScoped<IGetAppSettingsQuery, GetAppSettingsQuery>();
            services.AddScoped<ISaveAppSettingsCommand, SaveAppSettingsCommand>();
            services.AddTransient<ICreateServerCommand, CreateServerCommand>();
            //services.AddTransient<IUpdateServerCommand, UpdateServerCommand>();
            //services.AddTransient<IDeleteServerCommand, DeleteServerCommand>();
            services.AddScoped<IGetServerListQuery, GetServerListQuery>();
            //services.AddTransient<IGetServerByIdQuery, GetServerByIdQuery>();

            
            //services.AddScoped<IGetConfigByIdQuery<ServerConfiguration>, GetConfigByIdQuery<ServerConfiguration>>();
            //services.AddScoped<ISaveConfigCommand<ServerConfiguration>, SaveConfigCommand<ServerConfiguration>>();
            //services.AddScoped<IGetConfigByIdQuery<GameConfiguration>, GetConfigByIdQuery<GameConfiguration>>();
            //services.AddScoped<ISaveConfigCommand<GameConfiguration>, SaveConfigCommand<GameConfiguration>>();
            //services.AddScoped<IGetConfigByIdQuery<EventConfiguration>, GetConfigByIdQuery<EventConfiguration>>();
            //services.AddScoped<ISaveConfigCommand<EventConfiguration>, SaveConfigCommand<EventConfiguration>>();

            // repositories
            services.AddScoped<IDataRepository<AppSettings>, AppSettingsRepository>();
            services.AddScoped<IDataRepository<Server>, ServerRepository>();
            services.AddScoped<IDataRepository<ServerConfiguration>, ServerConfigurationRepository>();

            services.AddTransient<IConfigRepository<ServerConfiguration>, ServerConfigRepository>();
            services.AddTransient<IConfigRepository<GameConfiguration>, GameConfigRepository>();
            services.AddTransient<IConfigRepository<EventConfiguration>, EventConfigRepository>();

        }
    }
}
