using Aplicacao.Domain.ValueObject.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aplicacao.Infra.DataAccess.Map
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Endereco");

            builder
                .HasKey(pk => pk.Id)
                .HasName("EnderecoId");

            builder
                .Property(n => n.Country)
                .HasColumnName("pais")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(300)")
                .IsRequired();

            builder
                .Property(n => n.State)
                .HasColumnName("estado")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(300)")
                .IsRequired();

            builder
                .Property(n => n.City)
                .HasColumnName("cidade")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(300)")
                .IsRequired();

            builder
                .Property(n => n.Neighborhood)
                .HasColumnName("bairro")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(300)")
                .IsRequired();

            builder
                .Property(n => n.Street)
                .HasColumnName("rua")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(300)")
                .IsRequired();

            builder
                .Property(n => n.Number)
                .HasColumnName("numero")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(20)")
                .IsRequired();

            builder
                .Property(n => n.Complement)
                .HasColumnName("complemento")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(300)");

            builder
                .Property(n => n.ZipCode)
                .HasColumnName("cep")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(8)")
                .IsRequired();

            builder
                .HasOne(c => c.Customer)
                .WithMany(c => c.Address)
                .HasForeignKey(c => c.CustomerId);            
        }
    }
}