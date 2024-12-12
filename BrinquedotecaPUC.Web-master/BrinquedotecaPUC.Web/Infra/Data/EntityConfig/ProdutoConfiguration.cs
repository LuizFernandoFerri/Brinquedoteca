using BrinquedotecaPUC.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BrinquedotecaPUC.Web.Infra.Data.EntityConfig
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguration() { }

        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(e => e.IdProduto);

            builder.Property(e => e.IdProduto)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.CodProduto)
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar");

            builder.Property(e => e.IdCategoria)
                .HasColumnType("int");

            builder.Property(e => e.IdFaixaEtaria)
                .HasColumnType("int");

            builder.Property(e => e.IdEstConservacao)
                .HasColumnType("int");

            builder.Property(e => e.Quantidade)
                .HasColumnType("int");

            builder.Property(e => e.Status)
                .HasColumnType("int");

            builder.Property(e => e.Observacao)
                .HasMaxLength(500)
                .HasColumnType("varchar");

            builder.Property(u => u.RecCreatedBy)
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(t => t.RecCreatedOn)
                .HasColumnType("datetime");

            builder.Property(u => u.RecModifiedBy)
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(u => u.RecModifiedOn)
                .HasColumnType("datetime");

            builder.Ignore(u => u.ResultValidation);

            #region .: Relacionamentos :.

            builder.HasOne(c => c.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(c => c.IdCategoria);

            builder.HasOne(e => e.EstadoConservacao)
                .WithMany(ec => ec.Produtos)
                .HasForeignKey(e => e.IdEstConservacao);

            #endregion .: Relacionamentos :.
        }
    }
}
