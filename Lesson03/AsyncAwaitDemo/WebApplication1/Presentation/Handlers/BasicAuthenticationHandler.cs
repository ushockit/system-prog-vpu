using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        readonly IServiceManager serviceManager;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IServiceManager serviceManager)
            : base(options, logger, encoder, clock)
        {
            this.serviceManager = serviceManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var ep = Context.GetEndpoint();

            if (ep?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
                return AuthenticateResult.NoResult();
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing 'Authorization' header!");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var authData = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter))
                .Split(new[] { ':' }, 2);
            var login = authData[0];
            var pswd = authData[1];

            // TODO: Checking user in database
            var user = await serviceManager.PeopleService.GetPersonByFirstNameAndLastNameAsync(login, pswd);
            if (user is null)
                return AuthenticateResult.Fail("Invalid username or password");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }
    }
}
