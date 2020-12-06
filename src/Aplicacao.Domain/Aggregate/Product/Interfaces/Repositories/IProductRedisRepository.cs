using Aplicacao.Domain.Model;

namespace Aplicacao.Domain.Interfaces.Repositories
{
    public interface IProductRedisRepository : IRedisRepository<Product, int>
    {

    }
}