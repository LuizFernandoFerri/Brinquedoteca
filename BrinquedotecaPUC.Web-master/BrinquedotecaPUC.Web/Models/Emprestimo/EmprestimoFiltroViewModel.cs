using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Models
{
    public class EmprestimoFiltroViewModel
    {
        public int? IdRec { get; set; }
        public string? CodEmprestimo { get; set; }
        public string? NomeCliente { get; set; }
        public string? NomeBrinquedo {  get; set; }
        public string DataEmprestimo { get; set; }
    }
}
