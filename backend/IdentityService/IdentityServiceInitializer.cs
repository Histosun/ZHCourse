using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using ZS.JWT;

namespace ZSCourse.IdentityService
{
    public class IdentityServiceInitializer
    {
        public static void Init(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<IdDbContext>(options =>
            {
                var dbconfig = builder.Configuration.GetSection("Database");
                string connStr = dbconfig.GetSection("ConnStr").Value;
                options.UseNpgsql(connStr);
            });
            builder.Services.AddDataProtection();
            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            });
            var idBuilder = new IdentityBuilder(typeof(User), typeof(Role), builder.Services);
            idBuilder.AddEntityFrameworkStores<IdDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

            JWTOptions jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTOptions>();

            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddAuthentication(options =>
            {
                // makes controller to use our sheme as default
                options.DefaultChallengeScheme = ZSJWTDefaults.Schema;
            }).AddScheme<ZSJWTRedisAuthenticationOptions, ZSJWTRedisAuthenticationHandler>(ZSJWTDefaults.Schema, options =>
            {
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.ValidateLifetime = true;
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                options.TokenValidationParameters.ValidIssuer = jwtOpt.Issuer;
                options.TokenValidationParameters.ValidAudience = jwtOpt.Audience;
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.Key));
                options.ConnectionMultiplexer = builder.Services.BuildServiceProvider().GetService<IConnectionMultiplexer>();
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
            });
        }
    }
}
