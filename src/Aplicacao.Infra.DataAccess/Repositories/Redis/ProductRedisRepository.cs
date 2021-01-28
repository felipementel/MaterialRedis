using Aplicacao.Domain.Aggregate.Product.Interfaces.Repositories;
using Aplicacao.Domain.Aggregate.Product.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Aplicacao.Infra.DataAccess.Repositories.Redis
{
    public class ProductRedisRepository : BaseRedisRepository<Product, int>, IProductRedisRepository
    {
        public ProductRedisRepository(
            IDistributedCache distributedCache,
            IConfiguration configuration)
            : base(distributedCache, configuration) { }
    }
}