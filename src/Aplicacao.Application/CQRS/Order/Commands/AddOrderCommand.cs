using Aplicacao.Application.CQRS.Commands;
using Aplicacao.Application.DTOs;
using System.Reflection;

namespace Aplicacao.Application.CQRS.Order.Commands
{
    public class AddOrderCommand : OrderCommand
    {
        public const string CommandQueueName = "add-order-command-queue";
        public override string QueueName => CommandQueueName;
    }
}