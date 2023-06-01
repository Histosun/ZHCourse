using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace ZS.JWT;

public interface IZSValidator
{
    public AuthenticateResult Validate(string securityToken, TokenValidationParameters validationParameters);
}
