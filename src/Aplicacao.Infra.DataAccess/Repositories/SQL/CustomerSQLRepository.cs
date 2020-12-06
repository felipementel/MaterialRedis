using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Infra.DataAccess.Repositories.SQL
{
    public class CustomerSQLRepository : BaseSQLRepository<Customer, int>, ICustomerSQLServerRepository
    {
        public CustomerSQLRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
