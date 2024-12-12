using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.ValueObjects;
using BrinquedotecaPUC.Web.Models.Cliente;

namespace BrinquedotecaPUC.Web.Domain.Interfaces
{
    public interface IClienteService : IServiceBase<Cliente>
    {
        int ObterTotalClientes(string cpf, string nome, int idRec);
        ClienteGridViewModel ListarClientes(int pageNumber, int pageSize, ClienteFiltroViewModel filtroCliente);
        ClienteViewModel ObterCliente(int id);
        ValidationResult ExcluirCliente(int idRec);
        ClienteViewModel Salvar(ClienteViewModel clienteEntity);
        List<ClienteViewModel> ListaCodUsuarioLookup(string term);
    }
}
