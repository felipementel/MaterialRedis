using Aplicacao.Application.CQRS.Commands;
using Aplicacao.Application.DTOs;

namespace Aplicacao.Application.CQRS.Order.Commands
{
    public abstract class OrderCommand : Command
    {
        public OrderDTO OrderDTO { get; set; }
    }
}
