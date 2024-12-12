using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Models.Produto
{
    public class ProdutoFiltroViewModel
    {
        public int? IdProduto { get; set; }
        public string? CodProduto {  get; set; }
        public string? Descricao { get; set; }
        public int? IdCategoria { get; set; }
        public string? FaixaEtaria { get; set; }
        public int? IdEstConservacao { get; set; }
        public int? Status { get; set; }
    }
}
