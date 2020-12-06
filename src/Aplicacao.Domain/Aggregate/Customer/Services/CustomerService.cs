using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Interfaces.Services;
using Aplicacao.Domain.Model;
using Aplicacao.Domain.UoW;
using Aplicacao.Domain.Validations;
using System.Threading.Tasks;

namespace Aplicacao.Domain.Services
{
    public class CustomerService : BaseService<Customer, int>, ICustomerService
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
