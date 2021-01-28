using Aplicacao.Domain.Aggregate.Product.Model;
using Aplicacao.Domain.Interfaces.Repositories;

namespace Aplicacao.Domain.Aggregate.Product.Interfaces.Repositories
{
    public interface IProductSQLServerRepository : ISQLRepository<Model.Product, int> { }
}