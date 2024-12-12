using BrinquedotecaPUC.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrinquedotecaPUC.Web.Infra.Data.EntityConfig
{
    public class EmprestimoConfiguration : IEntityTypeConfiguration<Emprestimo>
    {
        public void Configure(EntityTypeBuilder<Emprestimo> builder)
        {
            builder.ToTable("Emprestimo");
            
            builder.HasKey(e => e.IdPedido);

            builder.Property(e => e.IdPedido)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.idCliente)
                .HasColumnType("int");

            builder.Property(e => e.CodUsuario)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("varchar");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(u => u.RecCreatedBy)
                .HasColumnName("usuarioCadastro")
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(t => t.RecCreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("dataCadastro");

            builder.Property(u => u.RecModifiedBy)
                .HasColumnName("usuarioAtualizacao")
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(u => u.RecModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("dataAtualizacao");

            builder.Ignore(u => u.ResultValidation);

            builder.HasMany(e => e.DetalhesEmprestimo)
                .WithOne(e => e.Emprestimo)
                .HasForeignKey(e => e.IdPedido);

            builder.HasOne(e => e.Cliente)
                .WithMany(e => e.Emprestimos)
                .HasForeignKey(e => e.idCliente);

        }
    }
}
