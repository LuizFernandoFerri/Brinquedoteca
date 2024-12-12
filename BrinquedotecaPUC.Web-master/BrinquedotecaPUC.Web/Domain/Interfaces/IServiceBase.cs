using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Domain.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity Update(TEntity obj);
        ValidationResult Remove(TEntity obj);
        void Dispose();
    }
}
