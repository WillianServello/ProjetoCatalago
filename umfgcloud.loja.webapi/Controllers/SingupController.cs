using Microsoft.AspNetCore.Mvc;
using umfgcloud.loja.aplicacao.service.Classes;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.webapi.Controllers
{
    /// <summary>
    /// endpoints para cadastrar usuário
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("[controller]")] //toda classe controller deve possuir o sufixo controller
    public sealed class SingupController : Controller
    {
        //a palavra reserva readonly permite que a variavel
        //tenha seu valor manipulado apenas em sua definição
        //<param>
        //ou dentro do método construtor
        private readonly IUsuarioServicos _servico;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servico"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SingupController(IUsuarioServicos servico)
        {
            _servico = servico ?? throw new ArgumentNullException(nameof(servico));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SingupAsync(UsuarioDTO.SingUpRequest dto)
        {
            try
            {
                await _servico.CadastrarAsync(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
