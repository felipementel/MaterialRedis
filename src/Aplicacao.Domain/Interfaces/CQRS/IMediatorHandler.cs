namespace Aplicacao.Domain.Interfaces.CQRS
{
    public interface IMediatorHandler
    {
        bool EnQueue<T>(T command, string queueName);
    }
}
