using BrinquedotecaPUC.Web.CrossCutting.Enumerators;
using BrinquedotecaPUC.Web.Domain.Entities;

namespace BrinquedotecaPUC.Web.Models.Produto
{
    public class ProdutoViewModel : BaseViewModel
    {
        public int? IdProduto { get; set; }
        public string? CodProduto { get; set; }
        public string? Descricao { get; set; }
        public int? IdCategoria { get; set; }
        public string? DescricaoCategoria
        {
            get
            {
                if (IdCategoria > 0 && Categoria != null)
                {
                    return Categoria.NomeCategoria;
                }
                return null;
            }
        }
        public int? IdFaixaEtaria { get; set; }
        public int? IdEstConservacao { get; set; }
        public string? EstConservacaoDescricao
        {
            get
            {
                if (IdEstConservacao > 0 && EstadoConservacao != null)
                {
                    return EstadoConservacao.Descricao;
                }
                return null;
            }
        }
        public int? Quantidade { get; set; }
        public int? Status { get; set; }
        public string? Observacao { get; set; }
        public string UrlImagem { get; set; }
        public string? StatusProduto
        {
            get
            {
                if (Status > 0)
                {
                    return EnumHelper.GetEnumDescription((StatusProduto)Status);
                }
                return null;
            }
        }

        public FileInfo? Imagem { get; set; }

        public virtual Categoria? Categoria { get; set; } = new Categoria();
        public virtual EstadoConservacao? EstadoConservacao { get; set; } = new EstadoConservacao();

        #region .: Propriedades da entidade :.

        public bool IsEdit { get; set; } = false;

        #endregion
    }
}
