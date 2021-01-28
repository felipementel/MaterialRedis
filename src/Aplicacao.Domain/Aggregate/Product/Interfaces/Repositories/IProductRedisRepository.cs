using Aplicacao.Domain.Interfaces.Repositories;

namespace Aplicacao.Domain.Aggregate.Product.Interfaces.Repositories
{
    public interface IProductRedisRepository : IRedisRepository<Model.Product, int> { }
}