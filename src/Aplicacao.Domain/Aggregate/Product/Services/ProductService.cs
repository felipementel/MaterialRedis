using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Interfaces.Services;
using Aplicacao.Domain.Model;
using Aplicacao.Domain.UoW;
using Aplicacao.Domain.Validations;

namespace Aplicacao.Domain.Services
{
    public class ProductService : BaseService<Product, int>, IProductService
    {
        private readonly IUnitOfWork _uow;

        private readonly IProductSQLServerRepository _sqlServerRepository;

        private readonly IProductRedisRepository _redisRepository;

        private readonly ProductValidator _validationRules;

        public ProductService(
            IUnitOfWork uow,
            IProductSQLServerRepository sqlServerRepository,
            IProductRedisRepository redisRepository,
            ProductValidator validationRules)
            : base(uow, sqlServerRepository, redisRepository, validationRules)
        {
            _uow = uow;
            _sqlServerRepository = sqlServerRepository;
            _redisRepository = redisRepository;
            _validationRules = validationRules;
        }
    }
}
