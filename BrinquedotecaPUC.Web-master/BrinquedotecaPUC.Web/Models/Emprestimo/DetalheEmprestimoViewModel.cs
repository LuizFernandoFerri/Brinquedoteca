using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Models.Produto;
using BrinquedotecaPUC.Web.CrossCutting.Enumerators;

namespace BrinquedotecaPUC.Web.Models.Emprestimo
{
    public class DetalheEmprestimoViewModel : EntityBase
    {
        public int? IdDetalhePedido { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProduto { get; set; }
        public DateTime? DataPedido { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public int? Status { get; set; }
        public string DescricaoStatus 
        {
            get
            {
                if (Status > 0)
                {
                    return EnumHelper.GetEnumDescription((StatusEmprestimoEnum)Status);
                }
                return string.Empty;
            }
        }

        public virtual ProdutoViewModel Produto { get; set; }

        public bool IsEdit { get; set; } = false;
        public bool IdDeleted { get; set; } = false;
    }
}
