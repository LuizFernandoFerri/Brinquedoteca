using BrinquedotecaPUC.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrinquedotecaPUC.Web.Infra.Data.EntityConfig
{
    public class DetalheEmprestimoConfiguration : IEntityTypeConfiguration<DetalheEmprestimo>
    {
        public void Configure(EntityTypeBuilder<DetalheEmprestimo> builder)
        {
            builder.ToTable("DetalheEmprestimo");
            builder.HasKey(e => e.IdDetalhePedido);

            builder.Property(e => e.IdDetalhePedido)
                .IsRequired()
                .HasColumnName("idDetalhePedido")
                .HasColumnType("int");

            builder.Property(e => e.IdPedido)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.IdProduto)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.DataPedido)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(e => e.DataDevolucao)
                .IsRequired()
                .HasColumnType("datetime");

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

            builder.HasOne(e => e.Emprestimo)
                .WithMany(e => e.DetalhesEmprestimo)
                .HasForeignKey(e => e.IdPedido);

            //builder.HasOne(e => e.Produto)
            //    .WithMany(e => e.DetalheEmprestimo)
            //    .HasForeignKey(e => e.IdProduto);

        }
    }
}
