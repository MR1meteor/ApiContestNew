﻿using ApiContestNew.Core.Interfaces.Repositories;
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
            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                return await Task.FromResult(AuthenticateResult.Success(
                    new AuthenticationTicket(new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new[] { 
                                new Claim(ClaimTypes.Email, string.Empty),
                                new Claim(ClaimTypes.Role, string.Empty) 
                            }, "Basic")), Scheme.Name)));
            }

            if (authorizationHeader != null &&
                authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authorizationHeader.Substring("Basic ".Length ).Trim();
                var encodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = encodedString.Split(':');

                var account = await _authenticationRepository.Authenticate(credentials[0], credentials[1]);
                if (account != null)
                {
                    var claims = new[] { 
                        new Claim(ClaimTypes.Email, credentials[0]),
                        new Claim(ClaimTypes.Role, account.Role) };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return await Task.FromResult(
                        AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }
            }

            Response.StatusCode = 401;
            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"none\"");
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Data"));
        }
    }
}
