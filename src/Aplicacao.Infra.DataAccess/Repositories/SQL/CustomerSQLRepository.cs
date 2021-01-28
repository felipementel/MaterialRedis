using Aplicacao.Domain.Aggregate.Customer.Model;
using Aplicacao.Domain.Aggregate.Customers.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Infra.DataAccess.Repositories.SQL
{
    public class CustomerSQLRepository : BaseSQLRepository<Customer, int>, ICustomerSQLServerRepository
    {
        public CustomerSQLRepository(DbContext dbContext) : base(dbContext) { }
    }
}