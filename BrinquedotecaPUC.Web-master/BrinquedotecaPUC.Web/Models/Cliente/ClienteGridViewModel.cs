using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Models.Cliente
{
    public class ClienteGridViewModel : ClienteFiltroViewModel
    {
        public List<ClienteViewModel> Clientes { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalClientes { get; set; }

        // Calcular o número total de páginas
        public int TotalPages => (int)Math.Ceiling((double)TotalClientes / PageSize);

        // Determinar se há uma página anterior
        public bool HasPreviousPage => PageNumber > 1;

        // Determinar se há uma próxima página
        public bool HasNextPage => PageNumber < TotalPages;

        public virtual ValidationResult ResultValidadion { get; set; }
    }
}
