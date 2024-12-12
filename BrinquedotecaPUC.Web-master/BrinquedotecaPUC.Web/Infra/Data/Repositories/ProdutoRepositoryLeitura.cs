using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class ProdutoRepositoryLeitura : RepositoryLeituraBase<Produto>, IProdutoRepositoryLeitura
    {
        public ProdutoRepositoryLeitura(BrinquedotecaContext context) : base(context) 
        { 
        
        }

        public List<Produto> ProdutoList(int pageNumber, int pageSize, string codProduto, string descricao, int? idEstConservacao, int? idProduto)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var query = dbSetBrinq.AsQueryable();

                if (!string.IsNullOrEmpty(codProduto))
                    query = query.Where(x => x.CodProduto.Contains(codProduto));

                if (!string.IsNullOrEmpty(descricao))
                    query = query.Where(x => x.Descricao.Contains(descricao));
                
                if (idEstConservacao > 0)
                    query = query.Where(x => x.IdEstConservacao == idEstConservacao.Value);
                
                if (idProduto > 0)
                    query = query.Where(x => x.IdProduto == idProduto.Value);

                var result = query
                    .Include(p => p.Categoria)
                    .Include(p => p.EstadoConservacao)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToList();

                return result;

            }
        }

        public Produto? GetProductByKey(int idProduto)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var result = dbSetBrinq.AsNoTracking().SingleOrDefault(c => c.IdProduto == idProduto);

                return result;
            }
        }
    }
}

