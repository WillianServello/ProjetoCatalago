using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Entidades;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.aplicacao.service.Classes
{
    internal class ProdutoServico : IProdutoServico
    {
        public async Task AdionarAsync(ProdutoDTO.ProdutoRequest dto)
        {
            var produto = new ProdutoEntity(userId, userEmail);
        }

        public Task AtualizarAsync(ProdutoDTO.ProdutoRequest dto)
        {
            throw new NotImplementedException();
        }

        public Task<ProdutoDTO.ProdutoResponse> ObterPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProdutoDTO.ProdutoResponse>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
