using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.Classes;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.aplicacao.service.Classes
{
    public sealed class UsuarioServico : IUsuarioServicos
    {
        private readonly string[] _roles = [Role.Desenvolvedor, Role.Admin, Role.Padrao];
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtOptions _jwtOptions;

        public UsuarioServico(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _jwtOptions = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<UsuarioDTO.SingInResponse> AutenticarAsync(UsuarioDTO.SingInRequest dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, true);

            if (!resultado.Succeeded)
            {
                if (resultado.IsLockedOut)
                    throw new InvalidOperationException("Conta bloqueada!");

                if (resultado.IsNotAllowed)
                    throw new InvalidOperationException("Conta sem permisão para login!");

                if (resultado.RequiresTwoFactor)
                    throw new InvalidOperationException("Confirme eu login com o segundo fato de autenticação!");

                throw new InvalidOperationException("Email ou Password inválidos");
            }

            var identifyUser = (await _userManager.FindByEmailAsync(dto.Email))
                ?? throw new ArgumentException("Usuário não encontrado!");
            var claims = new List<Claim>();

            //convesao do JWT
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, identifyUser.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, identifyUser.NormalizedEmail));
            var token = new JwtSecurityToken
            (
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_jwtOptions.AcessTokenExpiration),
                signingCredentials: _jwtOptions.SigningCredentials,
                claims: claims

            );

            return new UsuarioDTO.SingInResponse()
            {
                AcessToken = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }

        public async Task CadastrarAsync(UsuarioDTO.SingUpRequest dto)
        {
            var identifyUser = new IdentityUser()
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(identifyUser, dto.Password);

            if (!resultado.Succeeded && resultado.Errors.Any())
                throw new InvalidOperationException(string.Join("\n", resultado.Errors.SelectMany(x => x.Description)));

            foreach (var role in _roles)
            {
                if(!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            if ((await _roleManager.FindByNameAsync(Role.Padrao)) is null)
                throw new ArgumentException("Configuração padrão não cadastrada");

            await _userManager.AddToRoleAsync(identifyUser, Role.Padrao);
            await _userManager.SetLockoutEnabledAsync(identifyUser, true);
            
        }
    }
}
