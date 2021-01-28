using Aplicacao.Domain.Aggregate.Customers.Interfaces.Repositories;
using Aplicacao.Domain.Aggregate.Customers.Interfaces.Services;
using Aplicacao.Domain.Aggregate.Customers.Validations;
using Aplicacao.Domain.Services;
using Aplicacao.Domain.UoW;

namespace Aplicacao.Domain.Aggregate.Customers.Services
{
    public class CustomerService : BaseService<Customer.Model.Customer, int>, ICustomerService
    {
        private readonly IUnitOfWork _uow;

        private readonly ICustomerSQLServerRepository _sqlServerRepository;

        private readonly ICustomerRedisRepository _redisRepository;

        private readonly CustomerValidator _validationRules;

        public CustomerService(
            IUnitOfWork uow, 
            ICustomerSQLServerRepository sqlServerRepository,
            ICustomerRedisRepository redisRepository,
            CustomerValidator validationRules)
            : base(uow, sqlServerRepository, redisRepository, validationRules)
        {
            _uow = uow;
            _sqlServerRepository = sqlServerRepository;
            _redisRepository = redisRepository;
            _validationRules = validationRules;
        }
    }
}