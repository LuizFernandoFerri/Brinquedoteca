using BrinquedotecaPUC.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrinquedotecaPUC.Web.Infra.Data.EntityConfig
{
    public class EstadoConservacaoConfiguration : IEntityTypeConfiguration<EstadoConservacao>
    {
        public void Configure(EntityTypeBuilder<EstadoConservacao> builder)
        {
            builder.ToTable("EstadoConservacao");
            builder.HasKey(e => e.IdEstConservacao);
            builder.Property(e => e.IdEstConservacao).IsRequired().HasColumnType("int");
            builder.Property(e => e.Descricao).IsRequired().HasMaxLength(50).HasColumnType("varchar");
        }
    }
}
