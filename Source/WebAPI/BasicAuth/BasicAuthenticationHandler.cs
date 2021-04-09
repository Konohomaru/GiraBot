﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebAPI
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
        public IConfiguration Configuration { get; }

        public BasicAuthenticationHandler(
			IConfiguration configuration,
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock) : base(options, logger, encoder, clock)
		{
            Configuration = configuration;
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			string username;
			string password;

			try {
				var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
				var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
				var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

				username = credentials[0];
				password = credentials[1];
			} catch {
				return Task.FromResult(AuthenticateResult.Fail("Error Occured.Authorization failed."));
			}

			if (Configuration["Password"] != password)
                return Task.FromResult(AuthenticateResult.Fail("Invalid Credentials"));

            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
	}
}