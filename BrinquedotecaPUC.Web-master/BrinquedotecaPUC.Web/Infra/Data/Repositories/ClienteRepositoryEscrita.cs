using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;
using System.Transactions;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class ClienteRepositoryEscrita : RepositoryEscritaBase<Cliente>, IClienteRepositoryEscrita
    {
        public ClienteRepositoryEscrita(BrinquedotecaContext context) : base(context)
        {
        }

        public void Remover(Cliente cliente)
        {
            if (cliente != null)
            {
                dbSetBrinq.Remove(cliente);
            }
        }
    }
}
