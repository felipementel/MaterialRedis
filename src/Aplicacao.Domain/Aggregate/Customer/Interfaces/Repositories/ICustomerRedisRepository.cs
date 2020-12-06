using Aplicacao.Domain.Model;

namespace Aplicacao.Domain.Interfaces.Repositories
{
    public interface ICustomerRedisRepository : IRedisRepository<Customer, int>
    {

    }
}