using System;

namespace BrinquedotecaPUC.Web.Domain.Interfaces.Repositories
{
    public interface IRepositoryEscritaBase<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
