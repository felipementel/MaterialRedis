using Aplicacao.Application.CQRS.Commands;

namespace Aplicacao.Application.CQRS.Order.Commands
{
    public class UpdateOrderCommand : OrderCommand
    {
        public const string CommandQueueName = "update-order-command-queue";
        public override string QueueName => CommandQueueName;
    }
}