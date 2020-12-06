namespace Aplicacao.Application.CQRS.Order.Events
{
    public class OrderAddedEvent : OrderEvent
    {
        public const string EventQueueName = "order-added-event-queue";
        public override string QueueName => EventQueueName;
    }
}
