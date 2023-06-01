using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ZS.JWT;

public class ZSJWTAuthenticationOptions : AuthenticationSchemeOptions
{
    private readonly JwtSecurityTokenHandler _defaultHandler = new JwtSecurityTokenHandler();
    public ISecurityTokenValidator TokenValidator { get; private set; }
    public readonly TokenValidationParameters TokenValidationParameters = new TokenValidationParameters();

    public ZSJWTAuthenticationOptions()
    {
        TokenValidator = _defaultHandler;
    }
}