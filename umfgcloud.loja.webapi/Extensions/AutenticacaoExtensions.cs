using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using umfgcloud.loja.dominio.service.Classes;

namespace umfgcloud.loja.webapi.Extensions
{
    /// <summary>
    /// classe concreta é toda classe que eu utilizo para criar uma instancia em memoria
    /// classe estaticas e abstratas nao tem essa possibilidade 
    /// diferencao entre uma classe de extensao, concreta, abstract e static
    /// </summary>
    internal static class AutenticacaoExtensions
    {
        internal static void AddAutenticacao(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationSectionJwtOptions = configuration.GetSection(nameof(JwtOptions)).GetChildren();

            var issuer = configurationSectionJwtOptions
                .FirstOrDefault(x => x.Key == nameof(JwtOptions.Issuer))?.Value ?? string.Empty; 

            var audiance = configurationSectionJwtOptions
                .FirstOrDefault(x => x.Key == nameof(JwtOptions.Audience))?.Value ?? string.Empty; 

            var securityKey = configurationSectionJwtOptions
                .FirstOrDefault(x => x.Key == nameof(JwtOptions.SecurityKey))?.Value ?? string.Empty;

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));

            var tokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = false,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audiance,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,
            };

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = issuer;
                options.Audience = audiance;
                options.AcessTokenExpiration = int.Parse(
                    configurationSectionJwtOptions.FirstOrDefault(x => x.Key == nameof(JwtOptions.AcessTokenExpiration))?.Value ?? string.Empty);
                options.RefreshTokenExpiration = int.Parse(
                    configurationSectionJwtOptions.FirstOrDefault(x => x.Key == nameof(JwtOptions.RefreshTokenExpiration))?.Value ?? string.Empty);
                options.SigningCredentials =
                    new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            });

            //defini quais a caracteristicas de uma senha
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        } 
    }
}
