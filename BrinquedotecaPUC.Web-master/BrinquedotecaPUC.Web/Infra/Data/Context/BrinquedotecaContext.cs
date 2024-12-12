using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Infra.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Validation;

namespace BrinquedotecaPUC.Web.Infra.Data.Context
{
    public class BrinquedotecaContext : DbContext
    {
        //private readonly ISessionUser _sessionUser;

        public BrinquedotecaContext(DbContextOptions<BrinquedotecaContext> options) : base(options)
        {

        }

        public virtual DbSet<Usuario> dbUsuarios { get; set; }
        public virtual DbSet<Cliente> dbClientes { get; set; }
        public virtual DbSet<Produto> dbProdutos { get; set; }
        public virtual DbSet<Categoria> dbCategorias { get; set; }
        public virtual DbSet<EstadoConservacao> dbEstadoConservacao { get; set; }
        public virtual DbSet<Emprestimo> dbEmprestimo { get; set; }
        public virtual DbSet<DetalheEmprestimo> dbDetalheEmprestimo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddModelConfigurationCustom(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void AddModelConfigurationCustom(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new UsuariosConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new EstadoConservacaoConfiguration());
            modelBuilder.ApplyConfiguration(new EmprestimoConfiguration());
            modelBuilder.ApplyConfiguration(new DetalheEmprestimoConfiguration());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RecCreatedOn") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    //if (!string.IsNullOrEmpty(_sessionUser.UserName))
                    //{
                    //    entry.Property("RecCreatedBy").CurrentValue = _sessionUser.UserName;
                    //    entry.Property("RecModifiedBy").CurrentValue = _sessionUser.UserName;
                    //}
                    entry.Property("RecCreatedBy").CurrentValue = "SYSADM";
                    entry.Property("RecModifiedBy").CurrentValue = "SYSADM";
                    entry.Property("RecCreatedOn").CurrentValue = DateTime.Now;
                    entry.Property("RecModifiedOn").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("RecCreatedBy").IsModified = false;
                    entry.Property("RecCreatedOn").IsModified = false;
                    //if (!string.IsNullOrEmpty(_sessionUser.UserName))
                    //    entry.Property("RecModifiedBy").CurrentValue = _sessionUser.UserName;
                    entry.Property("RecModifiedBy").CurrentValue = "SYSADM";
                    entry.Property("RecModifiedOn").CurrentValue = DateTime.Now;
                }
            }

            /*
             * O bloco a baixo serve para capturar o erro no EntityValidationErrors
             */
            try
            {
                var result = base.SaveChanges();
                return result;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
