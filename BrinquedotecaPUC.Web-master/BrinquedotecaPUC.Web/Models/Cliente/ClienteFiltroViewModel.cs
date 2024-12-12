using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Models.Cliente
{
    public class ClienteFiltroViewModel
    {
        public int? IdRec { get; set; }
        public string? CodUsuario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
}
