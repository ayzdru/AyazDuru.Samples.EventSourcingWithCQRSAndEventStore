using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Events.TodoList
{
    public class TodoListCreated : IEvent
    {
        public Guid TodoListId { get; set; }
        public string Title { get; set; }
        public Colour Colour { get; set; }
    }
}
