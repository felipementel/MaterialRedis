using Aplicacao.Application.CQRS.Events;
using Aplicacao.Application.DTOs;

namespace Aplicacao.Application.CQRS.Order.Events
{
    public abstract class OrderEvent : Event
    {
        public OrderDTO OrderDTO { get; set; }
    }
}
