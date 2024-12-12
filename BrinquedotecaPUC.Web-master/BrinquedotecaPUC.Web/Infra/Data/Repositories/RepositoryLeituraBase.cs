using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class RepositoryLeituraBase<TEntity> : IRepositoryLeituraBase<TEntity>
            where TEntity : EntityBase
    {
        protected readonly BrinquedotecaContext dbContext;

        protected readonly DbSet<TEntity> dbSetBrinq;

        public RepositoryLeituraBase(BrinquedotecaContext context)
        {
            dbContext = context;
            dbSetBrinq = context.Set<TEntity>();
        }

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int take = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindReadOnly(Expression<Func<TEntity, bool>> predicate, int take = -1)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByKey(object[] key)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByKeyReadOnly(object[] key)
        {
            throw new NotImplementedException();
        }
    }
}
