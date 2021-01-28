using Aplicacao.Domain.Interfaces.Repositories;

namespace Aplicacao.Domain.Aggregate.Customers.Interfaces.Repositories
{
    public interface ICustomerSQLServerRepository : ISQLRepository<Customer.Model.Customer, int> { }
}