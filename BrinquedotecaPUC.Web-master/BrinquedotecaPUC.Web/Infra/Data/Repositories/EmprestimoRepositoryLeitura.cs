using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;
using System.Data.Entity;
using System.Transactions;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class EmprestimoRepositoryLeitura : RepositoryLeituraBase<Emprestimo>, IEmprestimoRepositoryLeitura
    {
        public EmprestimoRepositoryLeitura(BrinquedotecaContext context) : base(context)
        {
        }
        
        public List<Emprestimo> ListarEmprestimos(int pageNumber, int pageSize, string codUsuario, int? idEmprestimo, DateTime? dataEmprestimo, string codProduto, string descricao)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var query = dbSetBrinq.Include(x => x.DetalhesEmprestimo).AsQueryable();

                if (!string.IsNullOrEmpty(codUsuario))
                    query = query.Where(x => x.CodUsuario.Contains(codUsuario));

                if (idEmprestimo > 0)
                    query = query.Where(x => x.IdPedido == idEmprestimo);

                if (dataEmprestimo != null)
                    query = query.Where(x => x.DataEmprestimo == dataEmprestimo);

                //if (string.IsNullOrEmpty(codProduto))
                //    query = query.Where(x => x.DetalhesEmprestimo.Any(d => d.Produto.CodProduto == codProduto));

                //if (string.IsNullOrEmpty(descricao))
                //    query = query.Where(x => x.DetalhesEmprestimo.Any(d => d.Produto.Descricao == descricao));

                var result = query
                    .Include(p => p.DetalhesEmprestimo)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToList();

                return result;
            }
        }
    }
}
