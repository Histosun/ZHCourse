using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace ZS.JWT;

public class ZSJWTRedisValidatedContext: ResultContext<ZSJWTRedisAuthenticationOptions>
{
    public ZSJWTRedisValidatedContext(HttpContext context, AuthenticationScheme scheme, ZSJWTRedisAuthenticationOptions options): base(context, scheme, options) { }
    public SecurityToken SecurityToken { get; set; } = default!;
}
