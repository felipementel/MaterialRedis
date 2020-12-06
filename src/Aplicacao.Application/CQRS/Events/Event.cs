using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Application.CQRS.Events
{
    public abstract class Event
    {
        public abstract string QueueName { get; }
        public DateTime Timestamp { get; set; }

        public Event()
        {
            Timestamp = new DateTime();
        }
    }
}
