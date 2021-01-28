using Aplicacao.Domain.Aggregate.Product.Interfaces.Repositories;
using Aplicacao.Domain.Aggregate.Product.Interfaces.Services;
using Aplicacao.Domain.Aggregate.Product.Validations;
using Aplicacao.Domain.Services;
using Aplicacao.Domain.UoW;

namespace Aplicacao.Domain.Aggregate.Product.Services
{
    public class ProductService : BaseService<Model.Product, int>, IProductService
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