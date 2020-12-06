using Aplicacao.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aplicacao.Infra.DataAccess.Map
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("Produto");

            builder
                .HasKey(pk => pk.Id)
                .HasName("ProdutoId");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.Description)
                .HasColumnName("descricao")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(300)")
                .IsRequired();

            builder
                .Property(p => p.Weight)
                .HasColumnName("peso")
                .HasMaxLength(300)
                .HasColumnType<float>("float")
                .IsRequired();

            builder
                .Property(p => p.SKU)
                .HasColumnName("sku")
                .HasMaxLength(300)
                .HasColumnType<string>("varchar(300)")
                .IsRequired();

            builder
                .Property(p => p.Price)
                .HasColumnName("preco")
                .HasMaxLength(300)
                .HasColumnType<decimal>("decimal(10,2)")
                .IsRequired();
        }
    }
}
