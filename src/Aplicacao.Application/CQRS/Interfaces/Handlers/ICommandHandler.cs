using System.Threading.Tasks;

namespace Aplicacao.Application.CQRS.Interfaces.Handlers
{
    public interface ICommandHandler<T> where T : class
    {
        public Task Handle(T command);
    }
}
