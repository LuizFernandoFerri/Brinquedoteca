using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class ProdutoRepositoryEscrita : RepositoryEscritaBase<Produto>, IProdutoRepositoryEscrita
    {
        public ProdutoRepositoryEscrita(BrinquedotecaContext context) : base(context)
        {

        }

        public void Remover(Produto produto)
        {
            if (produto != null)
            {
                dbSetBrinq.Remove(produto);
            }
        }
    }
}
