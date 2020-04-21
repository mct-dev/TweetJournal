using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TweetJournal.Access.Authentication.Jwt;
using TweetJournal.Access.Authentication.User;

namespace TweetJournal.Access.Authentication
{
    public static class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<IJwtAccess, JwtAccess>();
            services.AddTransient<IUserAccess, UserAccess>();
            services.AddScoped<IAuthenticationAccess, AuthenticationAccess>();
            
            ConfigureJwtService(services, configuration);
            ConfigureIdentityOptions(services);
        }

        private static void ConfigureIdentityOptions(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options => { options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        private static void ConfigureJwtService(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSecret = configuration["JwtSettings:Secret"];
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(builder =>
            {
                builder.SaveToken = true;
                builder.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });
        }
    }
}