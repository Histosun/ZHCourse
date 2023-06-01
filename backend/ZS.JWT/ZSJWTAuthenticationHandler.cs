using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ZS.JWT;

public class ZSJWTAuthenticationHandler<ZSAuthOptions> : AuthenticationHandler<ZSAuthOptions> where ZSAuthOptions : ZSJWTAuthenticationOptions, new()
{
    public ZSJWTAuthenticationHandler(IOptionsMonitor<ZSAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        StringValues tokens;
        if(!Request.Headers.TryGetValue("Authentication", out tokens) || tokens.Count != 1)
            return Task.FromResult(AuthenticateResult.NoResult());

        var token = tokens.First();
        if(!Options.TokenValidator.CanReadToken(token))
            return Task.FromResult(AuthenticateResult.Fail(new InvalidDataException("Invalid token!")));
        var parameters = Options.TokenValidationParameters;
        SecurityToken? validatedToken;
        ClaimsPrincipal principal;

        try
        {
            principal = Options.TokenValidator.ValidateToken(token, parameters, out validatedToken);
        }
        catch(Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }

        var tokenValidatedContext = new ZSJWTValidatedContext(Context, Scheme, Options)
        {
            Principal = principal,
            SecurityToken = validatedToken
        };

        tokenValidatedContext.Success();
        return Task.FromResult(tokenValidatedContext.Result);
    }
}