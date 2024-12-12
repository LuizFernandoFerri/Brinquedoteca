namespace BrinquedotecaPUC.Web.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
