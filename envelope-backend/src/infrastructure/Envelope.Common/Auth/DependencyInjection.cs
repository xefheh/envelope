using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Envelope.Common.Auth
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEnvelopeAuth(this IServiceCollection services, IConfiguration configuration, string sectionName = "AuthOptions")
        {
            var section = configuration.GetSection(sectionName);

            if(section == null)
            {
                throw new NullReferenceException("Auth section not found!");
            }

            var audience = section["Audience"] ?? throw new NullReferenceException("Audience is not found!");
            var issuer = section["Issuer"] ?? throw new NullReferenceException("Issuer is not found!");
            var key = section["Key"] ?? throw new NullReferenceException("Key is not found!");

            services
                .AddAuthentication("default")
                .AddJwtBearer("default", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                }
             );

            return services;
        }
    }
}
