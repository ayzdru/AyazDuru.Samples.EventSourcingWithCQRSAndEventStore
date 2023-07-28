using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Common;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Events.TodoList;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Exceptions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Aggregates
{
    public class TodoListItemAggregate : BaseAggregate
    {
        public void Created(TodoListItem model)
        {
            if (CheckStreamName())
                throw new StreamNotFoundException();

            TodoListItemCreated todoListItemCreated = new()
            {
                TodoListItemId = model.Id,
                TodoListId = model.TodoListId,
                Title = model.Title,
                Description = model.Description,
                IsDone = model.IsDone
            };
            events.Add(todoListItemCreated);
        }
        public void Updated(Guid todoListItemId, bool isDone)
        {
            if (CheckStreamName())
                throw new StreamNotFoundException();

            TodoListItemUpdated todoListItemUpdated = new()
            {
                TodoListItemId = todoListItemId,        
                IsDone = isDone
            };
            events.Add(todoListItemUpdated);
        }
        public void Deleted(Guid todoListItemId)
        {
            if (CheckStreamName())
                throw new StreamNotFoundException();

            TodoListItemDeleted todoListItemDeleted = new()
            {
                TodoListItemId = todoListItemId
            };
            events.Add(todoListItemDeleted);
        }
    }
}
