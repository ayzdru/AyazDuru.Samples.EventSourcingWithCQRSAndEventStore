using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Common;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Events.TodoList;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Aggregates
{
    public class TodoListAggregate : BaseAggregate
    {
        public void Created(TodoList model)
        {
            if (CheckStreamName())
                throw new StreamNotFoundException();

            TodoListCreated todoListCreated = new()
            {
                Title = model.Title,
                Colour = model.Colour
            };
            events.Add(todoListCreated);
        }
        public void TitleChanged(Guid todoListId, string title)
        {
            if (CheckStreamName())
                throw new StreamNotFoundException();

            TodoListTitleChanged todoListTitleChanged = new()
            {
                TodoListId = todoListId,
                 Title = title
            };
            events.Add(todoListTitleChanged);
        }
        public void Deleted(Guid todoListId)
        {
            if (CheckStreamName())
                throw new StreamNotFoundException();

            TodoListDeleted todoListDeleted = new()
            {
                TodoListId = todoListId
            };
            events.Add(todoListDeleted);
        }
    }
}
