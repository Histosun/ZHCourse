using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ZS.JWT;

public class ZSJWTRedisAuthenticationHandler : ZSJWTAuthenticationHandler<ZSJWTRedisAuthenticationOptions>
{
    public ZSJWTRedisAuthenticationHandler(IOptionsMonitor<ZSJWTRedisAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        StringValues tokens;
        if(!Request.Headers.TryGetValue("Authentication", out tokens) || tokens.Count != 1)
            return AuthenticateResult.NoResult();
        var token = tokens.First();

        if(!Options.TokenValidator.CanReadToken(token))
            return AuthenticateResult.Fail(new InvalidDataException("Invalid token!"));
        var parameters = Options.TokenValidationParameters;
        SecurityToken validatedToken;
        ClaimsPrincipal principal;
        try
        {
            principal = Options.TokenValidator.ValidateToken(token, parameters, out validatedToken);
        }
        catch(Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }

        Claim idClaim = principal.Claims.First(it => { 
            if(it.Type == ClaimTypes.NameIdentifier)
            {
                return true;
            }
            return false;
        });

        if(idClaim == null)
            return AuthenticateResult.Fail("Token Error, No id found in Token!");

        string userId = idClaim.Value;

        if(!await ValidateJWTRedisAsync(userId, tokens))
            return AuthenticateResult.Fail("Token out dated!");

        var tokenValidatedContext = new ZSJWTRedisValidatedContext(Context, Scheme, Options) { Principal = principal, SecurityToken = validatedToken };

        tokenValidatedContext.Success();
        return tokenValidatedContext.Result;
    }

    protected virtual async Task<bool> ValidateJWTRedisAsync(string userId, string jwt)
    {
        var redisDB = Options.ConnectionMultiplexer.GetDatabase();
        string? redisJWT = await redisDB.StringGetAsync($"login:${userId}:jwt");
        if(redisJWT == null || !redisJWT.Equals(jwt)) return false;
        return true;
    }
}