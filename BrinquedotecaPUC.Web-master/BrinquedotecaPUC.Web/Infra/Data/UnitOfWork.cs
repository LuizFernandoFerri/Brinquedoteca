using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Infra.Data.Context;

namespace BrinquedotecaPUC.Web.Infra.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private BrinquedotecaContext _context;

        public UnitOfWork(BrinquedotecaContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

    }
}
