using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Model;
using Microsoft.Extensions.Caching.Distributed;

namespace Aplicacao.Infra.DataAccess.Repositories.Redis
{
    public class ProductRedisRepository : BaseRedisRepository<Product, int>, IProductRedisRepository
    {
        public ProductRedisRepository(IDistributedCache distributedCache) : base(distributedCache)
        {

        }
    }
}