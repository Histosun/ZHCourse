using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;

namespace ZS.JWT;

public class ZSJWTRedisAuthenticationOptions : ZSJWTAuthenticationOptions
{
    public IConnectionMultiplexer ConnectionMultiplexer { get; set; }
    public ZSJWTRedisAuthenticationOptions() : base()
    {
    }
}