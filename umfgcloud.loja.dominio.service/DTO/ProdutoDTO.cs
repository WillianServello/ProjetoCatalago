using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace umfgcloud.loja.dominio.service.DTO
{
    public sealed class ProdutoDTO
    {
        public abstract class AbstractProdutoDTO
        {
            [JsonPropertyName("descricao")]
            [Required(ErrorMessage ="O atributo descricao é obrigatorio")]
            public string Descricao { get; set; } = string.Empty;
            [JsonPropertyName("ean")]
            [Required(ErrorMessage = "O atributo EAN é obrigatorio")]
            public string EAN { get; set; } = string.Empty;
            [JsonPropertyName("valorCompra")]
            [Required(ErrorMessage = "O atributo ValorCompra é obrigatorio")]
            public decimal ValorCompra { get; set; } = decimal.Zero;
            [JsonPropertyName("valorVenda")]
            [Required(ErrorMessage = "O atributo ValorVenda é obrigatorio")]
            public decimal ValorVenda { get; set; } = decimal.Zero;
        }

        public abstract class AbstratProdutoWithIdDTO : AbstractProdutoDTO
        {
            [JsonPropertyName("descricao")]
            [Required(ErrorMessage = "O atributo descricao é obrigatorio")]
            public Guid Id { get; set; } = Guid.Empty;
        }

        public sealed class ProdutoRequest : AbstractProdutoDTO { }
        public sealed class ProdutoResquestWithId : AbstratProdutoWithIdDTO { }
        public sealed class ProdutoResponse : AbstractProdutoDTO { }
    }
}
