using Aplicacao.Domain.Aggregate.Product.Interfaces.Repositories;
using Aplicacao.Domain.Aggregate.Product.Model;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Infra.DataAccess.Repositories.SQL
{
    public class ProductSQLRepository : BaseSQLRepository<Product, int>, IProductSQLServerRepository
    {
        public ProductSQLRepository(DbContext dbContext) : base(dbContext) { }
    }
}