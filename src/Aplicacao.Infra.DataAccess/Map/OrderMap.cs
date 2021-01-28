using Aplicacao.Domain.Aggregate.Order.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aplicacao.Infra.DataAccess.Map
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Compra");

            builder
                .HasKey(pk => pk.Id)
                .HasName("CompraId");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(c => c.Customer);

            builder
               .HasMany(c => c.OrderItems)
               .WithOne(c => c.Order)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
