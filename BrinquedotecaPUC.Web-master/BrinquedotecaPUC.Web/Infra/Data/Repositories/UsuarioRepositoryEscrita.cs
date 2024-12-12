using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class UsuarioRepositoryEscrita : RepositoryEscritaBase<Usuario>, IUsuarioRepositoryEscrita
    {
        public UsuarioRepositoryEscrita(BrinquedotecaContext context) : base(context)
        {
        }
    }
}
