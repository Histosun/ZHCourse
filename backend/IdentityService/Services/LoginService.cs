using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZS.JWT;

namespace ZSCourse.IdentityService
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IOptions<JWTOptions> optJWT;
        private readonly IConnectionMultiplexer connectionMultiplexer;


        public LoginService(UserManager<User> userManager, RoleManager<Role> roleManager, IConnectionMultiplexer connectionMultiplexer, IOptions<JWTOptions> optJWT)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.optJWT = optJWT;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password)
        {
            User? user = await userManager.FindByNameAsync(userName);
            if (user == null) return (SignInResult.Failed, null);
            if (await userManager.IsLockedOutAsync(user)) return (SignInResult.LockedOut, null);
            var checkResult = await userManager.CheckPasswordAsync(user, password);
            if (!checkResult)
            {

                var r = await userManager.AccessFailedAsync(user);
                if (!r.Succeeded)
                {
                    throw new ApplicationException("AccessFailed failed");
                }
                return (SignInResult.Failed, null);
            }

            string token = await BuildTokenAsync(user);
            StoreJwtToRedis(user.Id.ToString(), token);
            return (SignInResult.Success, token);
        }

        private async Task<string> BuildTokenAsync(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return BuildToken(claims, optJWT.Value);
        }

        public string BuildToken(IEnumerable<Claim> claims, JWTOptions options)
        {
            TimeSpan ExpiryDuration = TimeSpan.FromSeconds(options.ExpireSeconds);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(options.Issuer, options.Audience, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<bool> CreateWorldAsync()
        {
            string userRoleName = "User";
            if(await roleManager.RoleExistsAsync(userRoleName))
            {
                Role userRole = new Role();
                userRole.Name = userRoleName;
            }
            string adminRoleName = "Admin";
            if (await roleManager.RoleExistsAsync(adminRoleName))
            {
                Role userRole = new Role();
                userRole.Name = adminRoleName;
            }

            string adminUserName = "admin";
            User user = await userManager.FindByNameAsync(adminUserName);
            if (user == null)
            {
                user = new User();
                user.UserName = adminUserName;
                var r = await userManager.CreateAsync(user, "YouNeverKnow!123");
                Debug.Assert(r.Succeeded);
            }

            if(!await userManager.IsInRoleAsync(user, userRoleName))
            {
                var r = await userManager.AddToRoleAsync(user, userRoleName);
                Debug.Assert(r.Succeeded);
            }

            if (!await userManager.IsInRoleAsync(user, adminRoleName))
            {
                var r = await userManager.AddToRoleAsync(user, adminRoleName);
                Debug.Assert(r.Succeeded);
            }
            return true;
        }

        private void StoreJwtToRedis(string userId, string token)
        {
            var redisDB = connectionMultiplexer.GetDatabase();
            if (!redisDB.StringSet($"login:${userId}:jwt", token))
                throw new Exception("Failed to Store Key");
        }

        public async Task LogoutAsnyc(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = tokenHandler.ReadJwtToken(token);
            Claim idClaim = jwt.Claims.First(it => {
                if (it.Type == ClaimTypes.NameIdentifier)
                    return true;
                return false;
            });
            string id = idClaim.Value;
            await RemoveJwtFromRedisAsync(id);
        }

        private async Task RemoveJwtFromRedisAsync(string userId)
        {
            var redisDB = connectionMultiplexer.GetDatabase();
            await redisDB.KeyDeleteAsync($"login:${userId}:jwt");
        }
    }
}
