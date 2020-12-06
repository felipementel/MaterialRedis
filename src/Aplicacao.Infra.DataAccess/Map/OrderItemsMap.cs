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
        }
    }
}
