using BrinquedotecaPUC.Web.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BrinquedotecaPUC.Web.Domain.Interfaces.Repositories
{
    public interface IClienteRepositoryEscrita : IRepositoryEscritaBase<Cliente>
    {
        void Remover(Cliente cliente);
    }
}
