using BrinquedotecaPUC.Web.CrossCutting.Enumerators;
using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Models.Cliente;
using BrinquedotecaPUC.Web.Models.Emprestimo;

namespace BrinquedotecaPUC.Web.Models
{
    public class EmprestimoViewModel : BaseViewModel
    {
        public int IdPedido { get; set; }
        public string CodUsuario { get; set; }
        public int Status { get; set; }
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
        public DateTime DataEmprestimo { get; set; }

        public virtual ClienteViewModel Cliente { get; set; }
        public virtual ICollection<DetalheEmprestimoViewModel> DetalhesEmprestimo { get; set; }

        #region .: Propriedades da entidade :.

        public bool IsEdit { get; set; } = false;

        #endregion
    }
}
