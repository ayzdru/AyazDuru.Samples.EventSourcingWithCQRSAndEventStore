using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Aggregates;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Notifications
{
    public class TodoListItemUpdatedNotification : INotification
    {
        public Guid TodoListItemId { get; set; }
        public bool IsDone { get; set; }
        public class TodoListItemUpdatedNotificationHandler : INotificationHandler<TodoListItemUpdatedNotification>
        {
            private readonly IAggregateRepository _aggregateRepository;
            private readonly TodoListItemAggregate _todoListItemAggregate;

            public TodoListItemUpdatedNotificationHandler(IAggregateRepository aggregateRepository, TodoListItemAggregate todoListItemAggregate)
            {
                _aggregateRepository = aggregateRepository;
                _todoListItemAggregate = todoListItemAggregate;
            }

            public async Task Handle(TodoListItemUpdatedNotification notification, CancellationToken cancellationToken)
            {
                _todoListItemAggregate.SetStreamName($"todolistitem-{notification.TodoListItemId}");
                _todoListItemAggregate.Updated(notification.TodoListItemId, notification.IsDone);

                await _aggregateRepository.SaveAsync(_todoListItemAggregate);
            }
        }
    }
}
