using Aplicacao.Application.DTOs;
using Aplicacao.Application.Interfaces;
using Aplicacao.Domain.Interfaces.Services;
using Aplicacao.Domain.Model;
using AutoMapper;

namespace Aplicacao.Application.Services
{
    public class CustomerAppService : BaseAppService<Customer, CustomerDTO, int>, ICustomerAppService
    {
        protected ICustomerService _appService;
        protected IMapper _mapper;

        public CustomerAppService(ICustomerService appService, IMapper mapper) : base(appService, mapper)
        {
            _appService = appService;
            _mapper = mapper;
        }
    }
}