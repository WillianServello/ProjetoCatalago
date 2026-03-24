using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umfgcloud.loja.dominio.service.Classes
{
    public sealed class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecurityKey { get; set; } = string.Empty;
        public int AcessTokenExpiration { get; set; } = 0;
        public int RefreshTokenExpiration { get; set; } = 0;
        public SigningCredentials? SigningCredentials { get; set; } = null;

    }
}
