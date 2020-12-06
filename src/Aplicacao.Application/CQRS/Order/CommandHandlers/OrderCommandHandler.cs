using Aplicacao.Application.CQRS.Interfaces.Handlers;
using Aplicacao.Application.CQRS.Order.Commands;
using Aplicacao.Application.CQRS.Order.Events;
using Aplicacao.Application.DTOs;
using Aplicacao.Application.Interfaces;
using Aplicacao.Application.Services;
using Aplicacao.Domain.Interfaces.CQRS;
using Aplicacao.Domain.Interfaces.Services;
using AutoMapper;
using System.Threading.Tasks;

namespace Aplicacao.Application.CQRS.Order.CommandHandlers
{
    public class OrderCommandHandler :
        //BaseAppCQRSService<Domain.Model.Order, OrderDTO, int>,
        ICommandHandler<AddOrderCommand>
    {
        private readonly IOrderService _appService;
        private readonly IMediatorHandler _bus;
        protected IMapper _mapper;

        public OrderCommandHandler(IOrderService appService, IMapper mapper, IMediatorHandler bus) //: base(appService, mapper)
        {
            _appService = appService;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task Handle(AddOrderCommand command)
        {
            var itemMap = _mapper.Map<OrderDTO, Domain.Model.Order>(command.OrderDTO);
            await _appService.Add(itemMap);

            _bus.EnQueue<OrderAddedEvent>(new OrderAddedEvent { OrderDTO = command.OrderDTO }, OrderAddedEvent.EventQueueName);
        }
    }
}