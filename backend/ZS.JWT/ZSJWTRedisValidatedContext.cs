using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace ZS.JWT;

public class ZSJWTValidatedContext: ResultContext<ZSJWTAuthenticationOptions>
{
    public ZSJWTValidatedContext(HttpContext context, AuthenticationScheme scheme, ZSJWTAuthenticationOptions options): base(context, scheme, options) { }
    public SecurityToken SecurityToken { get; set; } = default!;
}
