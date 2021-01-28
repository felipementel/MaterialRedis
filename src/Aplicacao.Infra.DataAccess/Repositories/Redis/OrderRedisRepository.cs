using Aplicacao.Domain.Aggregate.Order.Interfaces.Repositories;
using Aplicacao.Domain.Aggregate.Order.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Aplicacao.Infra.DataAccess.Repositories.Redis
{
    public class OrderRedisRepository : BaseRedisRepository<Order, int>, IOrderRedisRepository
    {
        public OrderRedisRepository(
            IDistributedCache distributedCache,
            IConfiguration configuration)
            : base(distributedCache, configuration) { }
    }
}