using BrinquedotecaPUC.Web.Domain.Entities;

namespace BrinquedotecaPUC.Web.Domain.Interfaces.Repositories
{
    public interface IClienteRepositoryLeitura : IRepositoryLeituraBase<Cliente>
    {
        int ObterTotalClientes(string cpf, string nome, int? idRec);
        List<Cliente> ListarClientes(int pageNumber, int pageSize, string codUsuario, string cpf, string nome, int? idRec);
        Cliente? LocalizaCliente(int idRec);
        Cliente? GetByKeyReadOnly(string cpf);
        Cliente? GetByKey(int idRec);
        List<Cliente>? ListaCodUsuarioLookup(string term);
        bool VerificaSePossuiMovimento(string codUsuario);
    }
}
