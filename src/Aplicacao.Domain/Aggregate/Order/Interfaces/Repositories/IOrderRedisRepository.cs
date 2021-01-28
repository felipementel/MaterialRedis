using Aplicacao.Domain.Interfaces.Repositories;

namespace Aplicacao.Domain.Aggregate.Order.Interfaces.Repositories
{
    public interface IOrderRedisRepository : IRedisRepository<Model.Order, int> { }
}