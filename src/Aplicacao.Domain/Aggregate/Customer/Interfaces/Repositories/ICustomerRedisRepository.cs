using Aplicacao.Domain.Interfaces.Repositories;

namespace Aplicacao.Domain.Aggregate.Customers.Interfaces.Repositories
{
    public interface ICustomerRedisRepository : IRedisRepository<Customer.Model.Customer, int> { }
}