using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Model;
using Microsoft.Extensions.Caching.Distributed;

namespace Aplicacao.Infra.DataAccess.Repositories.Redis
{
    public class CustomerRedisRepository : BaseRedisRepository<Customer, int>, ICustomerRedisRepository
    {
        public CustomerRedisRepository(IDistributedCache distributedCache) : base(distributedCache)
        {

        }
    }
}