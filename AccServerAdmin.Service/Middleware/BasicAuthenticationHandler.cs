using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Service.Middleware
{
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

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (username != _settings.Username || password != _settings.Password)
                    return AuthenticateResult.Fail("Invalid Username or Password");

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username.GetHashCode().ToString()),
                    new Claim(ClaimTypes.Name, username),
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);

            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }
}
