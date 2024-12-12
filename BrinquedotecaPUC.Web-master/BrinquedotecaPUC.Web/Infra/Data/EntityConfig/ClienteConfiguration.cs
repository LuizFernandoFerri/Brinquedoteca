using BrinquedotecaPUC.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrinquedotecaPUC.Web.Infra.Data.EntityConfig
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id)
                .HasColumnType("int")
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar");

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(14)
                .HasColumnType("varchar");

            builder.Property(c => c.DataNascimento)
                .HasColumnName("dataNascimento")
                .HasColumnType("date");

            builder.Property(c => c.Telefone)
                .HasMaxLength(11)
                .HasColumnType("varchar");

            builder.Property(c => c.Celular)
                .HasMaxLength(11)
                .HasColumnType("varchar");

            builder.Property(c => c.Cep)
                .HasMaxLength(8)
                .HasColumnType("varchar");

            builder.Property(c => c.Logradouro)
                .HasMaxLength(150)
                .HasColumnType("varchar");

            builder.Property(c => c.Numero)
                .HasMaxLength(6)
                .HasColumnType("varchar");

            builder.Property(c => c.Complemento)
                .HasMaxLength(30)
                .HasColumnType("varchar");

            builder.Property(c => c.Bairro)
                .HasMaxLength(40)
                .HasColumnType("varchar");

            builder.Property(c => c.Cidade)
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(c => c.Estado)
                .HasMaxLength(2)
                .HasColumnType("varchar");

            builder.Property(c => c.Situacao)
                .HasColumnType("bit");

            builder.Property(c => c.RecCreatedBy)
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(c => c.RecCreatedOn)
                .HasColumnType("datetime");

            builder.Property(c => c.RecModifiedBy)
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(c => c.RecModifiedOn)
                .HasColumnType("datetime");

            builder.Ignore(c => c.ResultValidation);
        }
    }
}
