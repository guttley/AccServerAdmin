using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Resouce;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Service.Middleware
{
    // dotnet Core 3.0 broke this as they made DefaultHttpRequest internal, so need to find another way to test it
    [ExcludeFromCodeCoverage]
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AppSettings _settings;

        public BasicAuthenticationHandler(
            IOptions<AppSettings> options,
            IOptionsMonitor<AuthenticationSchemeOptions> optionsMonitor,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(optionsMonitor, logger, encoder, clock)
        {
            _settings = options.Value;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                   return Task.FromResult(AuthenticateResult.Fail(Strings.MissingAuthHeader));

                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (!(username.EqualsText(_settings.Username) && password.EqualsText(_settings.Password)))
                    return Task.FromResult(AuthenticateResult.Fail(Strings.InvalidUserOrPass));

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username.GetHashCode().ToString()),
                    new Claim(ClaimTypes.Name, username),
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail(Strings.InvalidAuthHeader));
            }
        }
    }
}
