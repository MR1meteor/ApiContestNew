using ApiContestNew.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ApiContestNew.Core.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticationRepository authenticationRepository)
            : base(options, loggerFactory, encoder, clock)
        {
            _authenticationRepository = authenticationRepository;
        }

        protected async override  Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (authorizationHeader != null &&
                authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authorizationHeader.Substring("Basic ".Length ).Trim();
                var encodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = encodedString.Split(':');
                if (await _authenticationRepository.Authenticate(credentials[0], credentials[1]))
                {
                    var claims = new[] { new Claim("email", credentials[0]) };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }
            }

            Response.StatusCode = 401;
            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"joydipkanjilal.com\"");
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Data"));
        }
    }
}
