using BrinquedotecaPUC.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BrinquedotecaPUC.Web.Infra.Data.EntityConfig
{
    public class UsuariosConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public UsuariosConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.HasKey(u => u.CodUsuario);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar");

            builder.Property(u => u.Password)
                .IsRequired()
                .HasColumnName("senha")
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(u => u.Situacao)
                .HasColumnType("bit");

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

            builder.Ignore(u => u.SenhaInvalida);
            builder.Ignore(u => u.ResultValidation);
        }
    }
}
