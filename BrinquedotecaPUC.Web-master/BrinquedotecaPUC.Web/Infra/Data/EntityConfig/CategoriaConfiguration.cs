using BrinquedotecaPUC.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrinquedotecaPUC.Web.Infra.Data.EntityConfig
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {

        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(c => c.IdCategoria);
            builder.Property(c => c.IdCategoria).IsRequired().HasColumnType("int");
            builder.Property(c => c.NomeCategoria).HasColumnName("categoria").IsRequired().HasMaxLength(50).HasColumnType("varchar");
            builder.Property(c => c.Descricao).IsRequired().HasMaxLength(255).HasColumnType("varchar");
        }
    }
}
