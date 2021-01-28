using Aplicacao.Domain.Aggregate.Order.Interfaces.Repositories;
using Aplicacao.Domain.Aggregate.Order.Model;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Infra.DataAccess.Repositories.SQL
{
    public class OrderSQLRepository : BaseSQLRepository<Order, int>, IOrderSQLServerRepository
    {
        public OrderSQLRepository(DbContext dbContext) : base(dbContext) { }
    }
}