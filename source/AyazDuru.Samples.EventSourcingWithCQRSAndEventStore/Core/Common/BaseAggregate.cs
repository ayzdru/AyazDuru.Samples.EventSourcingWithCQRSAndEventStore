using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Common
{
    public abstract class BaseAggregate
    {
        protected readonly List<IEvent> events = new();
        public List<IEvent> GetEvents => events;
        public string StreamName { get; private set; }
        public void SetStreamName(string streamName)
            => StreamName = streamName;
        protected bool CheckStreamName()
            => string.IsNullOrEmpty(StreamName) || string.IsNullOrWhiteSpace(StreamName);
    }
}
