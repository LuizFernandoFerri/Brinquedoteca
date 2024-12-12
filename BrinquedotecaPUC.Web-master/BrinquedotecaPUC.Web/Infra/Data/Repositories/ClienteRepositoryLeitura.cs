using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace BrinquedotecaPUC.Web.Infra.Data.Repositories
{
    public class ClienteRepositoryLeitura : RepositoryLeituraBase<Cliente>, IClienteRepositoryLeitura
    {
        public ClienteRepositoryLeitura(BrinquedotecaContext context) : base(context)
        {
        }

        public int ObterTotalClientes(string cpf, string nome, int? idRec)
        {
            return dbSetBrinq.Select(x => (x.Cpf == cpf || x.Nome == nome || x.Id == idRec) || x.Id > 0).Count();
        }

        public List<Cliente> ListarClientes(int pageNumber, int pageSize, string codUsuario, string cpf, string nome, int? idRec)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var query = dbSetBrinq.AsQueryable();

                if (!string.IsNullOrEmpty(codUsuario))
                    query = query.Where(x => x.CodUsuario.Contains(codUsuario));

                if (!string.IsNullOrEmpty(cpf))
                    query = query.Where(x => x.Cpf == cpf);

                if (!string.IsNullOrEmpty(nome))
                    query = query.Where(x => x.Nome.Contains(nome));

                if (idRec.HasValue)
                    query = query.Where(x => x.Id == idRec.Value);

                var result = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToList();

                return result;
            }
        }

        public Cliente? LocalizaCliente(int idRec)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var result = dbSetBrinq.AsNoTracking().SingleOrDefault(c => c.Id == idRec);

                return result;
            }
        }

        public Cliente? GetByKeyReadOnly(string cpf)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                        new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return dbSetBrinq.AsNoTracking().SingleOrDefault(x => x.Cpf == cpf);
            }
        }

        public Cliente? GetByKey(int idRec)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                        new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return dbSetBrinq.AsNoTracking().SingleOrDefault(x => x.Id == idRec);
            }
        }

        public List<Cliente>? ListaCodUsuarioLookup(string term)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var query = dbSetBrinq.AsQueryable();

                query = query.Where(x => x.CodUsuario.Contains(term) || x.Nome.Contains(term));
                                
                var result = query
                    .Take(10)
                    .AsNoTracking()
                    .ToList();

                return result;
            }
        }

        public bool VerificaSePossuiMovimento(string codUsuario)
        {
            var sql = @"SELECT 
                    (SELECT COUNT(1) FROM RESERVA WHERE codUsuario = @codUsuario) + 
                    (SELECT COUNT(1) FROM EMPRESTIMO WHERE codUsuario = @codUsuario) AS Value";

            var count = dbContext.Database.SqlQueryRaw<int>(sql, new Microsoft.Data.SqlClient.SqlParameter("@codUsuario", codUsuario)).FirstOrDefault();

            return count > 0 ? true : false;
        }
    }
}
