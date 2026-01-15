using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace UserApi.Api.Auth;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _config;

    public BasicAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration config)
        : base(options, logger, encoder) 
    {
        _config = config;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

        if (!AuthenticationHeaderValue.TryParse(authHeaderValues, out var authHeader))
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));

        if (!"Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authentication Scheme"));

        if (string.IsNullOrWhiteSpace(authHeader.Parameter))
            return Task.FromResult(AuthenticateResult.Fail("Missing credentials"));

        try
        {
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            if (credentials.Length != 2)
                return Task.FromResult(AuthenticateResult.Fail("Invalid credentials format"));

            var username = credentials[0];
            var password = credentials[1];

            var expectedUser = _config["BasicAuth:Username"];
            var expectedPass = _config["BasicAuth:Password"];

            if (username != expectedUser || password != expectedPass)
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));

            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }
}
