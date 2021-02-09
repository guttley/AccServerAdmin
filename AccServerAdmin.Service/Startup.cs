using System;
using System.IO;
using System.Linq;
using AccServerAdmin.Application;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Application.Bop.Commands;
using AccServerAdmin.Application.Bop.Queries;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Application.Drivers.Commands;
using AccServerAdmin.Application.Drivers.Queries;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Entries.Queries;
using AccServerAdmin.Application.Results;
using AccServerAdmin.Application.Results.Commands;
using AccServerAdmin.Application.Results.Queries;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using AccServerAdmin.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using AccServerAdmin.Application.Sessions.Queries;
using AccServerAdmin.Application.Sessions.Commands;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Notifications.Results;

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
#if (USE_HTTPS)
            services.AddFluffySpoonLetsEncryptRenewalService(new LetsEncryptOptions()
            {
                Email = "gav@differently.net",
                UseStaging = true,
                Domains = new[] { Program.DomainToUse },
                TimeUntilExpiryBeforeRenewal = TimeSpan.FromDays(30),
                CertificateSigningRequest = new CsrInfo
                {
                    CountryName = "United Kingdom",
                    Locality = "UK",
                    Organization = "Simsport Racing",
                    OrganizationUnit = "Simsport Racing",
                    State = "UK"
                }
            });

            services.AddFluffySpoonLetsEncryptFileCertificatePersistence();
            services.AddFluffySpoonLetsEncryptFileChallengePersistence();
#endif

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
                    o.Conventions.AuthorizeAreaFolder("Configuration", "/Drivers");
                    o.Conventions.AuthorizeAreaFolder("Configuration", "/Servers");
                    o.Conventions.AuthorizeAreaFolder("Configuration", "/Tools");
                    o.Conventions.AuthorizeAreaPage("Configuration", "/Settings");
                });

            services.AddSignalR();

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
#if (USE_HTTPS)
                app.UseFluffySpoonLetsEncryptChallengeApprovalMiddleware();
#endif
            }

#if (USE_HTTPS)
            app.UseHttpsRedirection();
#endif
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapHub<ResultImportHub>("/hubs/resultImportHub");
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            /*
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "AccServerAdmin");

            if (!Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);

            dbPath = Path.Combine(dbPath, "AccServerAdmin.db3");

            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite($"DataSource={dbPath}"));
            */

            var config = GetConfiguration();
            var connectionString = config.GetConnectionString("AzureSqlDb");
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));
            services.AddDefaultIdentity<IdentityUser>(o => o.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            var provider = services.BuildServiceProvider();
            var dbContext = provider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();

            if (!dbContext.Drivers.Any(d => d.Id == ListData.AnonymousDriverId))
            {
                dbContext.Drivers.Add(
                    new Driver
                    {
                        Id = ListData.AnonymousDriverId,
                        Firstname = "Anonymous",
                        Lastname = "Driver"
                    });

                dbContext.SaveChanges();
            }

        }

        private static IConfigurationRoot GetConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            if (environment != null)
            {
                builder.AddJsonFile($"appsettings.{environment}.json", optional: true);
            }

            return builder.Build();
        }

        private void RegisterApplicationComponents(IServiceCollection services)
        {
            // Application components
            services.AddTransient<IJsonConverter, JsonDotNetConverter>();
            services.AddTransient<IFile, FileApiWrapper>();
            services.AddTransient<IDirectory, DirectoryApiWrapper>();
            services.AddTransient<IServerInstanceCleanUp, ServerInstanceCleanUp>();
            services.AddTransient<IServerConfigWriter, ServerConfigWriter>();
            services.AddTransient<IServerInstanceCreator, ServerInstanceCreator>();
            services.AddTransient<IServerPathResolver, ServerPathResolver>();
            services.AddTransient<IResultImporter, ResultImporter>();
            services.AddSingleton<IProcessManager, ProcessManager>();


            // Repositories
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<IEntryRepository, EntryRepository>();
            services.AddTransient<IDataRepository<AppSettings>, AppSettingsRepository>();
            services.AddTransient<IDataRepository<SessionConfiguration>, DataRepository<SessionConfiguration>>();
            services.AddTransient<IDataRepository<Entry>, DataRepository<Entry>>();
            services.AddTransient<IDriverEntryRepository, DriverEntryRepository>();
            services.AddTransient<IDataRepository<Session>, DataRepository<Session>>();
            services.AddTransient<IBopRepository, BopRepository>();
            services.AddTransient<IDataRepository<Session>, DataRepository<Session>>();
            services.AddTransient<IDataRepository<SessionCar>, DataRepository<SessionCar>>();
            services.AddTransient<IDataRepository<SessionCarDriver>, DataRepository<SessionCarDriver>>();
            services.AddTransient<IDataRepository<SessionPenalty>, DataRepository<SessionPenalty>>();
            services.AddTransient<IDataRepository<SessionLap>, DataRepository<SessionLap>>();
            services.AddTransient<IDataRepository<GlobalEntryList>, DataRepository<GlobalEntryList>>();

            // Commands/Queries
            services.AddTransient<IGetAppSettingsQuery, GetAppSettingsQuery>();
            services.AddTransient<ISaveAppSettingsCommand, SaveAppSettingsCommand>();
            
            services.AddTransient<ICreateServerCommand, CreateServerCommand>();
            services.AddTransient<IUpdateServerCommand, UpdateServerCommand>();
            services.AddTransient<IDeleteServerCommand, DeleteServerCommand>();
            services.AddTransient<IGetServerListQuery, GetServerListQuery>();
            services.AddTransient<IGetServerByIdQuery, GetServerByIdQuery>();
            services.AddTransient<IGetDuplicatePortQuery, GetDuplicatePortQuery>();
            services.AddTransient<ICopyServerCommand, CopyServerCommand>();

            services.AddTransient<IGetSessionByIdQuery, GetSessionByIdQuery>();
            services.AddTransient<ICreateSessionCommand, CreateSessionCommand>();
            services.AddTransient<IUpdateSessionCommand, UpdateSessionCommand>();
            services.AddTransient<IDeleteSessionCommand, DeleteSessionCommand>();

            services.AddTransient<ICreateDriverCommand, CreateDriverCommand>();
            services.AddTransient<IUpdateDriverCommand, UpdateDriverCommand>();
            services.AddTransient<IDeleteDriverCommand, DeleteDriverCommand>();
            services.AddTransient<IGetDriverListQuery, GetDriverListQuery>();
            services.AddTransient<IGetDriverByIdQuery, GetDriverByIdQuery>();
            
            services.AddTransient<IGetImportableResultsQuery, GetImportableResultsQuery>();
            services.AddTransient<IGetEntryByIdQuery, GetEntryByIdQuery>();
            services.AddTransient<IValidateEntryCommand, ValidateEntryCommand>();
            services.AddTransient<ICreateEntryCommand, CreateEntryCommand>();
            services.AddTransient<IUpdateEntryCommand, UpdateEntryCommand>();
            services.AddTransient<IDeleteEntryCommand, DeleteEntryCommand>();
            services.AddTransient<IAddDriverEntryCommand, AddDriverEntryCommand>();
            services.AddTransient<IDeleteDriverEntryCommand, DeleteDriverEntryCommand>();
            services.AddTransient<IGridResetCommand, GridResetCommand>();
            services.AddTransient<IMoveDriverEntryCommand, MoveDriverEntryCommand>();

            services.AddTransient<ICreateBopCommand, CreateBopCommand>();
            services.AddTransient<IUpdateBopCommand, UpdateBopCommand>();
            services.AddTransient<IDeleteBopCommand, DeleteBopCommand>();
            services.AddTransient<IGetBopListQuery, GetBopListQuery>();
            services.AddTransient<IGetBopByIdQuery, GetBopByIdQuery>();

            services.AddTransient<IServerSessionQuery, ServerSessionQuery>();
            services.AddTransient<ISessionResultQuery, SessionResultQuery>();
            services.AddTransient<ISessionLapsQuery, SessionLapsQuery>();
            services.AddTransient<ITrackLapsQuery, TrackLapsQuery>();
            services.AddTransient<ITracksWithSessionsQuery, TracksWithSessionsQuery>();
            services.AddTransient<IDeleteResultSessionCommand, DeleteResultSessionCommand>();

            services.AddTransient<IGetGlobalEntryListQuery, GetGlobalEntryListQuery>();
            services.AddTransient<IGetGlobalEntryListByIdQuery, GetGlobalEntryListByIdQuery>();
            services.AddTransient<ICreateGlobalEntryListCommand, CreateGlobalEntryListCommand>();
            services.AddTransient<IUpdateGlobalEntryListCommand, UpdateGlobalEntryListCommand>();
            services.AddTransient<IDeleteGlobalEntryListCommand, DeleteGlobalEntryListCommand>();
            
        }
    }
}
