using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Events
{
    public class TodoListItemUpdated : IEvent
    {
        public Guid TodoListItemId { get; set; }
        public bool IsDone { get; set; }     
    }
}
