using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Service.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Text;
using AccServerAdmin.Resouce;

namespace AccServerAdmin.Tests.Service.Middleware
{
    // dotnet Core 3.0 broke this as they made DefaultHttpRequest internal, so need to find another way to test it
    [ExcludeFromCodeCoverage]
    public class BasicAuthenticationTests
    {
        //[Test]
        public async Task TestNoAuthenticationHeader()
        {
            // Arrange
            var settings = new AppSettings { Username = "Test", Password = "qwert123"};
            var options = Substitute.For<IOptions<AppSettings>>();
            var optionsMonitor = Substitute.For<IOptionsMonitor<AuthenticationSchemeOptions>>();
            var logger = Substitute.For<ILoggerFactory>();
            var encoder = Substitute.For<UrlEncoder>();
            var clock = Substitute.For<ISystemClock>();
            options.Value.Returns(settings);

            var context = new DefaultHttpContext();
            var scheme = new AuthenticationScheme("basic", "basic", typeof(BasicAuthenticationHandler));
            var handler = new BasicAuthenticationHandler(options, optionsMonitor, logger, encoder, clock);
            await handler.InitializeAsync(scheme, context);

            // Act
            var authResult = await handler.AuthenticateAsync();

            // Assert
            Assert.That(authResult.Succeeded, Is.False);
            Assert.That(authResult.Failure, Is.Not.Null);
            Assert.That(authResult.Failure.Message, Is.EqualTo(Strings.MissingAuthHeader));
        }

        //[Test]
        public async Task TestInvalidAuthenticationHeader()
        {
            // Arrange
            var settings = new AppSettings { Username = "Test", Password = "qwert123" };
            var options = Substitute.For<IOptions<AppSettings>>();
            var optionsMonitor = Substitute.For<IOptionsMonitor<AuthenticationSchemeOptions>>();
            var logger = Substitute.For<ILoggerFactory>();
            var encoder = Substitute.For<UrlEncoder>();
            var clock = Substitute.For<ISystemClock>();
            options.Value.Returns(settings);

            var context = new DefaultHttpContext();
            var credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("NotTest" + "x" + "010101"));
            context.Request.Headers.Add("Authorization", $"Basic {credentials}");

            var scheme = new AuthenticationScheme("basic", "basic", typeof(BasicAuthenticationHandler));
            var handler = new BasicAuthenticationHandler(options, optionsMonitor, logger, encoder, clock);
            await handler.InitializeAsync(scheme, context);

            // Act
            var authResult = await handler.AuthenticateAsync();

            // Assert
            Assert.That(authResult.Succeeded, Is.False);
            Assert.That(authResult.Failure, Is.Not.Null);
            Assert.That(authResult.Failure.Message, Is.EqualTo(Strings.InvalidAuthHeader));
        }

        //[Test]
        public async Task TestIncorrectCredentials()
        {
            // Arrange
            var settings = new AppSettings { Username = "Test", Password = "qwert123" };
            var options = Substitute.For<IOptions<AppSettings>>();
            var optionsMonitor = Substitute.For<IOptionsMonitor<AuthenticationSchemeOptions>>();
            var logger = Substitute.For<ILoggerFactory>();
            var encoder = Substitute.For<UrlEncoder>();
            var clock = Substitute.For<ISystemClock>();
            options.Value.Returns(settings);

            var context = new DefaultHttpContext();
            var credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("NotTest" + ":" + "010101"));
            context.Request.Headers.Add("Authorization", $"Basic {credentials}");

            var scheme = new AuthenticationScheme("basic", "basic", typeof(BasicAuthenticationHandler));
            var handler = new BasicAuthenticationHandler(options, optionsMonitor, logger, encoder, clock);
            await handler.InitializeAsync(scheme, context);

            // Act
            var authResult = await handler.AuthenticateAsync();

            // Assert
            Assert.That(authResult.Succeeded, Is.False);
            Assert.That(authResult.Failure, Is.Not.Null);
            Assert.That(authResult.Failure.Message, Is.EqualTo(Strings.InvalidUserOrPass));
        }

        //[Test]
        public async Task TestAuthenticatedSuccess()
        {
            // Arrange
            var settings = new AppSettings { Username = "Test", Password = "qwert123" };
            var options = Substitute.For<IOptions<AppSettings>>();
            var optionsMonitor = Substitute.For<IOptionsMonitor<AuthenticationSchemeOptions>>();
            var logger = Substitute.For<ILoggerFactory>();
            var encoder = Substitute.For<UrlEncoder>();
            var clock = Substitute.For<ISystemClock>();
            options.Value.Returns(settings);

            var context = new DefaultHttpContext();
            var credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{settings.Username}:{settings.Password}"));
            context.Request.Headers.Add("Authorization", $"Basic {credentials}");

            var scheme = new AuthenticationScheme("basic", "basic", typeof(BasicAuthenticationHandler));
            var handler = new BasicAuthenticationHandler(options, optionsMonitor, logger, encoder, clock);
            await handler.InitializeAsync(scheme, context);

            // Act
            var authResult = await handler.AuthenticateAsync();

            // Assert
            Assert.That(authResult.Succeeded, Is.True);
        }

    }
}
