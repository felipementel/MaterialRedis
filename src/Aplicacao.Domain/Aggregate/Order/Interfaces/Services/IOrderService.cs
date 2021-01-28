using Aplicacao.Domain.Interfaces.Services;

namespace Aplicacao.Domain.Aggregate.Order.Interfaces.Services
{
    public interface IOrderService : IService<Model.Order, int> { }
}