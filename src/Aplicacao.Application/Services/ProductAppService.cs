using Aplicacao.Application.DTOs;
using Aplicacao.Application.Interfaces;
using Aplicacao.Domain.Aggregate.Product.Interfaces.Services;
using Aplicacao.Domain.Aggregate.Product.Model;
using AutoMapper;

namespace Aplicacao.Application.Services
{
    public class ProductAppService : BaseAppService<Product, ProductDTO, int>, IProductAppService
    {
        protected IProductService _appService;
        protected IMapper _mapper;

        public ProductAppService(IProductService appService, IMapper mapper) : base(appService, mapper)
        {
            _appService = appService;
            _mapper = mapper;
        }
    }
}