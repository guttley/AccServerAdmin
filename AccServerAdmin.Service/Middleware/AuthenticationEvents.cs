using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using Microsoft.Extensions.Options;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace AccServerAdmin.Service.Middleware
{
    public class AuthenticationEvents : BasicAuthenticationEvents
    {
        private readonly AppSettings _settings;

        public AuthenticationEvents(IOptions<AppSettings> options)
        {
            _settings = options.Value;
        }


        /// <inheritdoc/>
        public override Task ValidatePrincipalAsync(ValidatePrincipalContext context)
        {
            if (context.UserName == _settings.Username && context.Password == _settings.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, context.UserName, context.Options.ClaimsIssuer)
                };

                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, BasicAuthenticationDefaults.AuthenticationScheme));
                context.Principal = principal;
            }

            return Task.CompletedTask;
        }

    }
}
