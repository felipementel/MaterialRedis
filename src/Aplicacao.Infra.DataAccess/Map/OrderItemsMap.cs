using Aplicacao.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aplicacao.Infra.DataAccess.Map
{
    public class OrderItemsMap : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.ToTable("CompraItem");

            builder
                .HasKey(pk => pk.Id)
                .HasName("CompraItemId");

            builder
                .HasOne(c => c.Order)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(c => c.OrderId);

            builder
                .Property(p => p.Quantity)
                .HasColumnName("quantidade")
                .HasMaxLength(300)
                .HasColumnType<int>("int")
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
