using Aplicacao.Application.CQRS.Order.Commands;

namespace Aplicacao.Application.CQRS.Order.Interfaces.Handlers
{
    public interface IProductCommandHandler
    {
        public void Handle(AddOrderCommand command);
        public void Handle(UpdateOrderCommand command);
    }
}
