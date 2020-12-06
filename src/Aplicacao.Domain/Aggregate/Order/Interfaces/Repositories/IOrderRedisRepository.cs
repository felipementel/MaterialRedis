using Aplicacao.Domain.Model;

namespace Aplicacao.Domain.Interfaces.Repositories
{
    public interface IOrderRedisRepository : IRedisRepository<Order, int>
    {

    }
}