using Aplicacao.Domain.Interfaces.Repositories;

namespace Aplicacao.Domain.Aggregate.Order.Interfaces.Repositories
{
    public interface IOrderSQLServerRepository : ISQLRepository<Model.Order, int> { }
}