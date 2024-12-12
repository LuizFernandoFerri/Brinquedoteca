using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Transactions;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class UsuarioRepositoryLeitura : RepositoryLeituraBase<Usuario>, IUsuarioRepositoryLeitura
    {
        public UsuarioRepositoryLeitura(BrinquedotecaContext context) : base(context)
        {
            
        }

        public List<Usuario> ListarUsuarios()
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var result = dbSetBrinq.Where(x => x.CodUsuario != null).AsNoTracking().ToList();

                return result;
            }
        }

        public Usuario LocalizaUsuario(string userName, string userMail)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var result = dbSetBrinq.Where(x => x.CodUsuario == userName || x.Email == userMail).AsNoTracking().FirstOrDefault();

                return result;
            }
        }

        public Usuario? GetByKeyReadOnly(string codUsuario)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                        new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return dbSetBrinq.AsNoTracking().SingleOrDefault(x => x.CodUsuario == codUsuario);
            }
        }
    }
}
