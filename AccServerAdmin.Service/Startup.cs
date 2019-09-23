using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Application.Common;
using Castle.Facilities.AspNetCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Server;
using AccServerAdmin.Service.Controllers;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Service.Middleware;
using Microsoft.AspNetCore.Authentication;
using Swashbuckle.AspNetCore.Swagger;

namespace AccServerAdmin.Service
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private static readonly WindsorContainer Container = new WindsorContainer();
        private const string ApiTitle = "ACC Server Admin API";
        private const string ApiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = ApiTitle, Version = ApiVersion });
                c.AddSecurityDefinition("basic", new BasicAuthScheme { Type = "basic", Description = "basic authentication" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "basic", new string[] { } }, });
            });

            RegisterApplicationComponents();

            services.AddWindsor(Container,
                opts => opts.UseEntryAssembly(typeof(ServerController).Assembly), 
                () => services.BuildServiceProvider(validateScopes: false));

            ValidateConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiTitle); });
        }

        private void ValidateConfiguration()
        {
            var validator = Container.Resolve<ConfigValidator>();
            validator.Execute();
        }

        private void RegisterApplicationComponents()
        {
            // Application components
            Container.Register(Component.For<IHttpContextAccessor>().ImplementedBy<HttpContextAccessor>());
            Container.Register(Component.For<IJsonConverter>().ImplementedBy<JsonConverter>());
            Container.Register(Component.For<IFile>().ImplementedBy<FileApiWrapper>());
            Container.Register(Component.For<IDirectory>().ImplementedBy<DirectoryApiWrapper>());
            Container.Register(Component.For<IServerDirectoryResolver>().ImplementedBy<ServerDirectoryResolver>());
            Container.Register(Component.For<ConfigValidator>());

            // Server components
            Container.Register(Component.For<ICreateServerCommand>().ImplementedBy<CreateServerCommand>());
            Container.Register(Component.For<IUpdateServerCommand>().ImplementedBy<UpdateServerCommand>());
            Container.Register(Component.For<IDeleteServerCommand>().ImplementedBy<DeleteServerCommand>());
            Container.Register(Component.For<IGetServerListQuery>().ImplementedBy<GetServerListQuery>());
            Container.Register(Component.For<IGetServerByIdQuery>().ImplementedBy<GetServerByIdQuery>());
            Container.Register(Component.For<IServerRepository>().ImplementedBy<ServerRepository>());

            // Config components
            Container.Register(Component.For<IGetConfigByIdQuery<ServerConfiguration>>().ImplementedBy<GetConfigByIdQuery<ServerConfiguration>>());
            Container.Register(Component.For<ISaveConfigCommand<ServerConfiguration>>().ImplementedBy<SaveConfigCommand<ServerConfiguration>>());
            Container.Register(Component.For<IGetConfigByIdQuery<GameConfiguration>>().ImplementedBy<GetConfigByIdQuery<GameConfiguration>>());
            Container.Register(Component.For<ISaveConfigCommand<GameConfiguration>>().ImplementedBy<SaveConfigCommand<GameConfiguration>>());
            Container.Register(Component.For<IGetConfigByIdQuery<EventConfiguration>>().ImplementedBy<GetConfigByIdQuery<EventConfiguration>>());
            Container.Register(Component.For<ISaveConfigCommand<EventConfiguration>>().ImplementedBy<SaveConfigCommand<EventConfiguration>>());
            Container.Register(Component.For<IGetConfigByIdQuery<SessionConfiguration>>().ImplementedBy<GetConfigByIdQuery<SessionConfiguration>>());
            Container.Register(Component.For<ISaveConfigCommand<SessionConfiguration>>().ImplementedBy<SaveConfigCommand<SessionConfiguration>>());

        }

    }
}
