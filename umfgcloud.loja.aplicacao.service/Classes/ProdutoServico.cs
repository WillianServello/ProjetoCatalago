using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Entidades;
using umfgcloud.loja.dominio.service.Interfaces.Repositorios;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.aplicacao.service.Classes
{
    public class ProdutoServico : AbstractServico, IProdutoServico
    {
        private readonly IProdutoRepositorio _repositorio;
        public ProdutoServico(IHttpContextAccessor httpContextAcessor, IProdutoRepositorio repositorio) : base(httpContextAcessor)
        {
            _repositorio = repositorio;
        }

        public async Task AdicionarAsync(ProdutoDTO.ProdutoRequest dto)
        {
            var produto = new ProdutoEntity(UserId, UserEmail);

            //dto transita os dados inerentes a tablea
            produto.SetDescricao(dto.Descricao);
            produto.SetEAN(dto.EAN);
            produto.SetValorCompra(dto.ValorCompra);
            produto.SetValorVenda(dto.ValorVenda);

            await _repositorio.AdicionarAsync(produto);

        }

        public async Task AtualizarAsync(ProdutoDTO.ProdutoResquestWithId dto)
        {
            var produto = await _repositorio.ObterPorIdAsync(dto.Id);

            //dto transita os dados inerentes a tablea
            produto.SetDescricao(dto.Descricao);
            produto.SetEAN(dto.EAN);
            produto.SetValorCompra(dto.ValorCompra);
            produto.SetValorVenda(dto.ValorVenda);

            produto.Update(UserId, UserEmail);

            await _repositorio.AtualizarAsync(produto);
        }

        public async Task<ProdutoDTO.ProdutoResponse> ObterPorIdAsync(Guid id)
            =>  (await _repositorio.ObterPorIdAsync(id)).Adapt<ProdutoDTO.ProdutoResponse>();

        public async Task<IEnumerable<ProdutoDTO.ProdutoResponse>> ObterTodosAsync()
    => (await _repositorio.ObterTodosAsync()).Adapt<IEnumerable<ProdutoDTO.ProdutoResponse>>();

        public async Task RemoverAsync(Guid id)
         => await _repositorio.RemoverAsync(await _repositorio.ObterPorIdAsync(id));
    
    }
}
