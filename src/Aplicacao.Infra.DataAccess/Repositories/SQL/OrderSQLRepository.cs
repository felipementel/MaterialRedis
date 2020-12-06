using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Infra.DataAccess.Repositories.SQL
{
    public class OrderSQLRepository : BaseSQLRepository<Order, int>, IOrderSQLServerRepository
    {
        public OrderSQLRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
