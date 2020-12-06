using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Model;
using Microsoft.Extensions.Caching.Distributed;

namespace Aplicacao.Infra.DataAccess.Repositories.Redis
{
    public class OrderRedisRepository : BaseRedisRepository<Order, int>, IOrderRedisRepository
    {
        public OrderRedisRepository(IDistributedCache distributedCache) : base(distributedCache)
        {

        }
    }
}