using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aplicacao.Domain.Model;
using System;

namespace Aplicacao.Infra.DataAccess.Seed
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Produto");

            builder.HasData(new Product(1, "IPhone 12", 200, Guid.NewGuid().ToString().Split("-")[0], 8799.22M));
        }
    }
}
