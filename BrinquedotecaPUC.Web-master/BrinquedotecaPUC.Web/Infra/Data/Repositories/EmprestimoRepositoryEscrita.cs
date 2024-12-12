using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class EmprestimoRepositoryEscrita : RepositoryEscritaBase<Emprestimo>, IEmprestimoRepositoryEscrita
    {
        public EmprestimoRepositoryEscrita(BrinquedotecaContext context) : base(context)
        {
        }

        public void Remover(Emprestimo emprestimo)
        {
            if (emprestimo != null)
            {
                dbSetBrinq.Remove(emprestimo);
            }
        }
    }
}
