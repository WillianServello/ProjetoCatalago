using Microsoft.AspNetCore.Mvc;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.webapi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public sealed class SigninController : ControllerBase
    {
        private readonly IUsuarioServicos _servico;

        public SigninController(IUsuarioServicos servico)
        {
            _servico = servico ?? throw new ArgumentNullException(nameof(servico));
        }

        /// <summary>
        ///  Efetua o login do usuário
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IActionResult> AutenticaAsync(UsuarioDTO.SingInRequest dto)
        {
            try
            {
                return Ok(await _servico.AutenticarAsync(dto));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
