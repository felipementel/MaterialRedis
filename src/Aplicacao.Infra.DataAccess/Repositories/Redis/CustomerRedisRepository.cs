using Aplicacao.Domain.Aggregate.Customer.Model;
using Aplicacao.Domain.Aggregate.Customers.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Aplicacao.Infra.DataAccess.Repositories.Redis
{
    public class CustomerRedisRepository : BaseRedisRepository<Customer, int>, ICustomerRedisRepository
    {
        public CustomerRedisRepository(
            IDistributedCache distributedCache,
            IConfiguration configuration)
            : base(distributedCache, configuration) { }
    }
}