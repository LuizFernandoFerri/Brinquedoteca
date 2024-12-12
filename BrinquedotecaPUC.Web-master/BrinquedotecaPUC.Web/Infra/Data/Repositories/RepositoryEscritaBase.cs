using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class RepositoryEscritaBase<TEntity> : IRepositoryEscritaBase<TEntity> where TEntity : EntityBase
    {
        protected readonly BrinquedotecaContext dbContext;

        protected readonly DbSet<TEntity> dbSetBrinq;

        public RepositoryEscritaBase(BrinquedotecaContext context)
        {
            dbContext = context;
            dbSetBrinq = context.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            var entityReturn = dbSetBrinq.Add(entity);
            return entityReturn.Entity;
        }

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Remove(TEntity entity)
        {
            var entry = dbContext.Entry(entity);
            dbSetBrinq.Remove(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update(TEntity entity)
        {
            var entry = dbContext.Entry(entity);
            dbSetBrinq.Attach(entity);
            entry.State = EntityState.Modified;
        }
    }
}
