using Aplicacao.Application.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Application.CQRS.Interfaces.Events
{
    public interface IEventHandler<T> where T : Event
    {
        public void Handle(T domainEvent);
    }
}
