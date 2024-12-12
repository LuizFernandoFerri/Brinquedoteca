using System.Linq.Expressions;

namespace BrinquedotecaPUC.Web.Domain.Interfaces.Repositories
{
    public interface IRepositoryLeituraBase<TEntity> : IDisposable where TEntity : class
    {
        TEntity GetByKey(object[] key);
        TEntity GetByKeyReadOnly(object[] key);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int take = -1);
        IEnumerable<TEntity> FindReadOnly(Expression<Func<TEntity, bool>> predicate, int take = -1);
    }
}
